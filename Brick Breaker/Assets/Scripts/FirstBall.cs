using UnityEngine;
using System;
using Unity.Mathematics;

public class FirstBall : MonoBehaviour
{
    [SerializeField] private Ball _ballPrefab;

    private Transform _transform;
    private Rigidbody2D _rb;
    private float _velocity;

    private void Awake()
    {
        _transform = Instantiate(_ballPrefab).transform;
        _rb = _transform.GetComponent<Rigidbody2D>();
        _velocity = _transform.GetComponent<Ball>().Speed;
    }

    private void Update()
    {
        _transform.position = transform.position + Vector3.up / 3;

        if (Input.GetMouseButtonDown(0) == false)
            return;

        _rb.velocity = Vector2.up * _velocity;
        Destroy(this);
    }
}
