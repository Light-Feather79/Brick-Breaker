using System;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))]
// [RequireComponent(typeof(CircleCollider2D))]
public class BonusBlackStar : MonoBehaviour
{
    public static event Action BlackStarGained;

    private float _speed = 150;
    
    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        CircleCollider2D col = GetComponent<CircleCollider2D>();

        rb.AddForce(Vector2.up * _speed);
        rb.gravityScale = 1f;
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Paddle>(out Paddle paddle))
        {
            BlackStarGained?.Invoke();
            Destroy(gameObject);
        }
    }
}
