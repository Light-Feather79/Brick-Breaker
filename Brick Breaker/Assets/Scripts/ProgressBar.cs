using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private TextMeshProUGUI _text;
    private float _maxStarsForArea = 15;
    private float _currentStarsForArea;

    private void Start()
    {
        _bar.fillAmount = _currentStarsForArea / _maxStarsForArea;
        _text.text = "Progress: " + Math.Round((float)(_currentStarsForArea / _maxStarsForArea), 3) * 100f + "%";
    }

}
