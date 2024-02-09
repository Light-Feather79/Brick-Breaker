using UnityEngine;
using System;
using Unity.Mathematics;

public class FirstBall : MonoBehaviour
{
    [SerializeField] private Ball _ballPrefab;

    private Transform _ballTransform;
    private Rigidbody2D _BallRb;
    private float _ballVelocity;

    private void Awake()
    {
        _ballTransform = Instantiate(_ballPrefab).transform;
        _BallRb = _ballTransform.GetComponent<Rigidbody2D>();
        _ballVelocity = _ballTransform.GetComponent<Ball>().Speed;
    }

    private void Update()
    {
        _ballTransform.position = transform.position + Vector3.up / 2;

        if (Input.GetMouseButtonDown(0) == false)
            return;

        _BallRb.velocity = Vector2.up * _ballVelocity;
        Destroy(this);
    }
}
