using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BonusBalls : MonoBehaviour
{
    private float _speed = 150;
    private BallPooler _ballPooler;
    
    private void Awake()
    {
        _ballPooler = FindObjectOfType<BallPooler>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        CircleCollider2D col = GetComponent<CircleCollider2D>();

        rb.AddForce(Vector2.down * _speed);
        rb.gravityScale = .0f;
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Paddle>(out Paddle paddle))
        {
            MultiplyBalls();
            Destroy(gameObject);
        }
    }

    private void MultiplyBalls()
    {
        List<Ball> balls = FindObjectsOfType<Ball>().ToList();

        foreach (Ball ball in balls)
        {
            Ball pulledBall = _ballPooler.Pull();
            pulledBall.transform.position = ball.transform.position;
            pulledBall.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * pulledBall.Speed;
        }
    }
}
