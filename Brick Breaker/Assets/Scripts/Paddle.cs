using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class Paddle : MonoBehaviour
{
    [SerializeField] private float _screenWidthInUnits = 16;

    private float minX = 1f;
    private float maxX = 15f;
    private float _axisY;
    private float _ballVelocity = 10;
    private Transform _transform;
    private CapsuleCollider2D _collider;

    private void Start()
    {
        _transform = transform;
        _axisY = _transform.position.y;
        _collider = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ball>(out Ball ball))
            CorrectBallBounce(ball.GetComponent<Rigidbody2D>(), ball.transform.position);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float currentMousePos = Input.mousePosition.x / Screen.width * _screenWidthInUnits;
        float clampedMousePos = Mathf.Clamp(currentMousePos, minX, maxX);

        _transform.position = new Vector2(clampedMousePos, _axisY);
    }

    private void CorrectBallBounce(Rigidbody2D ballRb, Vector3 ballPosition)
    {
        float ballOffsetToPaddleCenter = _collider.ClosestPoint(ballPosition).x - _collider.bounds.center.x;
        float paddleHalfSize = _collider.bounds.size.x / 2;
        float velocityPercentX = ballOffsetToPaddleCenter / paddleHalfSize;

        if (ballOffsetToPaddleCenter >= 0)
            ballRb.velocity = new Vector2(_ballVelocity * velocityPercentX, Mathf.Abs(ballRb.velocity.y));
        else
            ballRb.velocity = new Vector2(_ballVelocity * velocityPercentX, Mathf.Abs(ballRb.velocity.y));
    }
}
