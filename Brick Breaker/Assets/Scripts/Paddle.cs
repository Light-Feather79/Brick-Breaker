using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class Paddle : MonoBehaviour
{
    [SerializeField] private float _screenWidthInUnits = 16;
    [SerializeField] private float _speed = 15f;
    
    private float _minX = 1f;
    private float _maxX = 15f;
    private float _ballVelocity = 10;
    private float _startingYPos;
    private Vector2 _startingScale;
    private float _randomPaddleOffset;
    private Transform _transform;
    private Transform _ballTransform;
    private CapsuleCollider2D _collider;
    private GameSession _gameSession;
    private Coroutine _wideningCoroutine;

    private void Start()
    {
        _transform = transform;
        _startingYPos = _transform.position.y;
        _startingScale = _transform.localScale;
        _collider = GetComponent<CapsuleCollider2D>();
        _gameSession = FindObjectOfType<GameSession>();
        _ballTransform = FindObjectOfType<Ball>().transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ball>(out Ball ball))
        {
            _randomPaddleOffset = UnityEngine.Random.Range(-1f, 1f);
            CorrectBallBounce(ball.GetComponent<Rigidbody2D>(), ball.transform.position, ball);
        }
    }

    void Update()
    {
        if (_gameSession.IsAutoPlay == false)
            Move();
        else
            MoveAutomatically();
    }

    private void MoveAutomatically()
    {
        Vector2 ballPos = new Vector2(_ballTransform.position.x + _randomPaddleOffset, _startingYPos);
        _transform.position = Vector2.Lerp(_transform.position, ballPos, _speed * Time.deltaTime);
    }

    private void Move()
    {
        float clampedMousePos = Mathf.Clamp(Input.mousePosition.x / Screen.width * _screenWidthInUnits, _minX, _maxX);
        _transform.position = new Vector2(clampedMousePos, _startingYPos);
    }


    private void CorrectBallBounce(Rigidbody2D ballRb, Vector3 ballPosition, Ball ball)
    {
        float ballOffsetToPaddleCenter = _collider.ClosestPoint(ballPosition).x - _collider.bounds.center.x;
        float paddleHalfSize = _collider.bounds.size.x / 2;
        float velocityPercentX = ballOffsetToPaddleCenter / paddleHalfSize;

        if (ballOffsetToPaddleCenter >= 0)
            ballRb.velocity = new Vector2(_ballVelocity * velocityPercentX, Mathf.Abs(ballRb.velocity.y));
        else
            ballRb.velocity = new Vector2(_ballVelocity * velocityPercentX, Mathf.Abs(ballRb.velocity.y));

        ballRb.velocity = ballRb.velocity.normalized * ball.Speed;
    }

    public void WidenY()
    {
        float widenedMultiplier = 2f;
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
