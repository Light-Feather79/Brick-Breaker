using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class Paddle : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    [SerializeField, Range(0.1f, 5f)] private float _gameSpeed;
    [SerializeField] private bool _isAutoPlay;
    
    private float _minX;
    private float _maxX;
    private float _startingYPos;
    private Vector2 _startingScale;
    private float _randomPaddleOffset;
    private Transform _transform;
    private Transform _ballTransform;
    private CapsuleCollider2D _collider;
    private Coroutine _wideningCoroutine;

    private float PaddleHalfSize => _collider.bounds.size.x / 2;

    private void OnEnable()
    {
        BonusWiden.Widen += WidenY;
    }

    private void OnDisable()
    {
        BonusWiden.Widen -= WidenY;
    }

    private void Start()
    {
        // _isAutoPlay = true;
        // _gameSpeed = 5f;
        // Time.timeScale = _gameSpeed;










        _transform = transform;
        _startingYPos = _transform.position.y;
        _startingScale = _transform.localScale;

        _collider = GetComponent<CapsuleCollider2D>();

        SetMinAndMaxX();
    }

    private void SetMinAndMaxX()
    {
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;

        _minX = -halfWidth + PaddleHalfSize;
        _maxX =  halfWidth - PaddleHalfSize;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ball>(out Ball ball))
        {
            _randomPaddleOffset = UnityEngine.Random.Range(-0.5f, 0.5f);
            CorrectBallBounce(ball.GetComponent<Rigidbody2D>(), ball.transform.position, ball.Speed);
        }
    }

    void Update()
    {
        if (_ballTransform == null)
            _ballTransform = FindObjectOfType<Ball>().transform;
            
        if (_isAutoPlay == false)
            Move();
        else
            MoveAutomatically();
    }

    private void Move()
    {
        float clampedMousePos = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, _minX, _maxX );
        _transform.position = new Vector2(clampedMousePos, _startingYPos);
    }

    private void MoveAutomatically()
    {
        Vector2 ballPos = new Vector2(_ballTransform.position.x + _randomPaddleOffset, _startingYPos);
        _transform.position = Vector2.Lerp(_transform.position, ballPos, _speed * Time.deltaTime);
    }

    private void CorrectBallBounce(Rigidbody2D ballRb, Vector3 ballPosition, float ballVelocity)
    {
        float ballOffsetToPaddleCenter = _collider.ClosestPoint(ballPosition).x - _collider.bounds.center.x;
        float velocityPercentX = ballOffsetToPaddleCenter / PaddleHalfSize;

        ballRb.velocity = new Vector2(ballVelocity * velocityPercentX, Mathf.Abs(ballRb.velocity.y));
        ballRb.velocity = ballRb.velocity.normalized * ballVelocity;
    }

    private void WidenY()
    {
        float widenedMultiplier = 1.5f;
        Vector2 widenedPaddle = Vector2.up * _startingScale *  widenedMultiplier + Vector2.right * _startingScale;


        if (_wideningCoroutine != null)
            StopCoroutine(_wideningCoroutine);

        _wideningCoroutine = StartCoroutine(WidenYRoutine(widenedPaddle));
    }

    private IEnumerator WidenYRoutine(Vector2 widenedPaddle)
    {
        _transform.localScale = widenedPaddle;
        yield return new WaitForSeconds(5f);
        _transform.localScale = _startingScale;
    }
}
