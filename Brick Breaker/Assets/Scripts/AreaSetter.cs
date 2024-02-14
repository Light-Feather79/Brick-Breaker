using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AreaSetter : MonoBehaviour
{
    [SerializeField] private Sprite[] _amountOfStars;
    [SerializeField] private GameObject _buttonLevels;
    private Button[] _levels;

    private void Start()
    {
        Enum.TryParse(gameObject.tag, out Area areaNumber);
        GameData.Data.GetAreaInfo(areaNumber);
        _levels = _buttonLevels.GetComponentsInChildren<Button>();

        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].GetComponent<Image>().sprite = _amountOfStars[i];
        }

    }

    public void DestroyArea()
    {
        Destroy(gameObject);
    }
}
