using UnityEngine;
using System;
using Unity.Mathematics;
using System.Collections;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float _speed = 50f;
    [SerializeField] private bool _x;
    [SerializeField] private bool _y;
    [SerializeField] private bool _z = true;

    private void Update()
    {
        transform.Rotate(   _speed * Convert.ToInt32(_x) * Time.deltaTime,
                            _speed * Convert.ToInt32(_y) * Time.deltaTime,
                            _speed * Convert.ToInt32(_z) * Time.deltaTime);
    }
}

