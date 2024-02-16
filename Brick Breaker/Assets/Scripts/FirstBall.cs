using UnityEngine;
using System;
using Unity.Mathematics;

public class FirstBall : MonoBehaviour
{
    [SerializeField] private Ball _ballPrefab;

    private Transform _transformBall;
    private Rigidbody2D _rbBall;
    private float _speedBall;

    private void Awake()
    {
        _transformBall = Instantiate(_ballPrefab).transform;
        _rbBall = _transformBall.GetComponent<Rigidbody2D>();
        _speedBall = _transformBall.GetComponent<Ball>().Speed;
    }

    private void Update()
    {
        _transformBall.position = transform.position + Vector3.up / 3;

        if (Input.GetMouseButtonDown(0) == false)
            return;

        _rbBall.velocity = Vector2.up * _speedBall;
        Destroy(this);
    }
}
