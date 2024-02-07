using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle _paddle;
    [SerializeField] private float _speed = 10f;

    private float _modifiedPush = 1f;
    private float _borderToModify = .3f;

    private Vector3 _paddleToBallVector;
    private Transform _transform;
    private Transform _paddleTransform;
    private Rigidbody2D _rb;
    private AudioSource _audioSource;
    private bool _isLaunched;

    private void Start()
    {
        _transform = transform;
        _paddleTransform = _paddle.transform;
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();

        _paddleToBallVector = _transform.position - _paddleTransform.position;
    }

    private void FixedUpdate()
    {
        if (_isLaunched)
            _rb.velocity = _rb.velocity.normalized * _speed;
    }

    private void Update()
    {
        if (_isLaunched == false)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _audioSource.PlayDelayed(0f);

        if (collision.gameObject.TryGetComponent<Paddle>(out Paddle paddle) == false)
            CorrectEndlessLoop();
    }

    private void CorrectEndlessLoop()
    {
        int[] randomOnes = new int[2] { 1, -1 };
        int randomOne = randomOnes[UnityEngine.Random.Range(0, randomOnes.Length)];
        _modifiedPush *= randomOne;

        if (_rb.velocity.x <= _borderToModify && _rb.velocity.x >= -_borderToModify)
            _rb.velocity = new Vector2(_modifiedPush, _rb.velocity.y);
        if (_rb.velocity.y <= _borderToModify && _rb.velocity.y >= -_borderToModify)
            _rb.velocity = new Vector2(_rb.velocity.x, _modifiedPush);
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _rb.velocity = new Vector2(0f, _speed);
            _isLaunched = true;
        }
    }

    private void LockBallToPaddle() => _transform.position = _paddleToBallVector + _paddleTransform.position;
}
