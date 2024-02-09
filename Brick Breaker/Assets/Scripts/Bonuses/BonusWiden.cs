using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BonusWiden : MonoBehaviour
{
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
            paddle.WidenY();
            Destroy(gameObject);
        }
    }
}
