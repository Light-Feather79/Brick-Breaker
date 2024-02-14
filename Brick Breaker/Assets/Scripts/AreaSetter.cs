using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AreaSetter : MonoBehaviour
{
    [SerializeField] private Sprite[] _imageOfStars;
    [SerializeField] private GameObject _buttonLevels;

    private LevelLoader[] _levels;
    private int _spaceBeforeNumerEng = 5;

    private void Start()
    {
        Enum.TryParse(gameObject.tag, out Area areaNumber);
        _levels = _buttonLevels.GetComponentsInChildren<LevelLoader>();

        for (int i = 0; i < _levels.Length; i++)
        {
            int stars = GameData.Instance.GetAreaInfo(_levels[i].Level);
            _levels[i].GetComponent<Image>().sprite = _imageOfStars[stars];
            _levels[i].GetComponentInChildren<TextMeshProUGUI>().text = _levels[i].Level.ToString().Insert(_spaceBeforeNumerEng, " ");

        }

    }

    public void DestroyArea()
    {
        Destroy(gameObject);
    }
}
