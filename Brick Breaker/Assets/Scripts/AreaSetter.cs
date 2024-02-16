using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AreaSetter : MonoBehaviour
{
    public Area Area;
    
    [SerializeField] private Sprite[] _imageOfStars;
    [SerializeField] private GameObject _buttonLevels;

    private LevelLoader[] _levels;
    private int _spaceBeforeNumerEng = 5;


    private void Start()
    {
        int areaNumber = (int)Area;
        int maxLevels = GameData.Maxlevels;
        int levelInArea = areaNumber * maxLevels - maxLevels + 1;

        _levels = _buttonLevels.GetComponentsInChildren<LevelLoader>();

        for (int i = 0; i < _levels.Length; i++, levelInArea++)
        {
            _levels[i].Level = (Level)levelInArea;
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
