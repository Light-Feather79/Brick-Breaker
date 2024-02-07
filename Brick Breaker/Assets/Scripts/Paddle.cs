using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float _screenWidthInUnits = 16;

    private float minX = 1f;
    private float maxX = 15f;
    private float _axisY;
    private Transform _transform;


    private void Start()
    {
        _transform = transform;
        _axisY = _transform.position.y;
    }


    void Update()
    {
        float currentMousePos = Input.mousePosition.x / Screen.width * _screenWidthInUnits;
        float clampedMousePos = Mathf.Clamp(currentMousePos, minX, maxX);

        _transform.position = new Vector2(clampedMousePos, _axisY);
    }
}
