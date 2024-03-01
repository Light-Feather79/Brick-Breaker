using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BonusWiden : MonoBehaviour
{
    public static event Action Widen;

    private float _speed = 150;
    
    private void Awake()
    {
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
            Widen?.Invoke();
            Destroy(gameObject);
        }
    }
}

