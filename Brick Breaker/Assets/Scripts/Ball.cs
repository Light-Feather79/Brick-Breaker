using UnityEngine;
using System;
using Unity.Mathematics;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    public static event Action<bool> BallLifeCycle;

    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _rotation = 5f;

    private float _borderToModify = .5f;
    private Rigidbody2D _rb;
    private AudioSource _audioSource;
    private Transform _transform;

    public float Speed { get => _speed;}

    private void OnEnable()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        BallLifeCycle?.Invoke(true);
    }

    private void OnDisable()
    {
        BallLifeCycle?.Invoke(false);
    }

    private void Update()
    {
        if (_rb.velocity.x > 0)
            _transform.Rotate(Vector3.forward * -_rotation);
        else
            _transform.Rotate(Vector3.forward * _rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _audioSource.PlayDelayed(0f);

        if (collision.gameObject.TryGetComponent<Paddle>(out Paddle paddle) == false)
            CorrectEndlessLoop();
    }

    private void CorrectEndlessLoop()
    {
        int[] velocities = new int[2] { 1, -1 };
        int randomVelocity = velocities[UnityEngine.Random.Range(0, velocities.Length)];

        if (_rb.velocity.x <= _borderToModify && _rb.velocity.x >= -_borderToModify)
            _rb.velocity = new Vector2(randomVelocity, _rb.velocity.y);
        if (_rb.velocity.y <= _borderToModify && _rb.velocity.y >= -_borderToModify)
            _rb.velocity = new Vector2(_rb.velocity.x, randomVelocity);

        _rb.velocity = _rb.velocity.normalized * Speed;
    }
}
