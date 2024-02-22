using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AreaSetter : MonoBehaviour
{
    public Area Area;

    [SerializeField] public ComicSO[] _allComicParts;
    
    [SerializeField] private Sprite[] _imageOfStars;
    [SerializeField] private GameObject _buttonLevels;

    private LevelLoader[] _levels;
    private int _spaceBeforeNumerEng = 5;

    public Level LastLevel {get; private set;}

    private void Start()
    {
        int levelInArea = FindFirstLevelInArea();

        _levels = _buttonLevels.GetComponentsInChildren<LevelLoader>();

        for (int i = 0; i < _levels.Length; i++, levelInArea++)
        {
            _levels[i].Level = (Level)levelInArea;
            int stars = GameData.Instance.GetStarAmountOfLevel(_levels[i].Level);
            _levels[i].GetComponent<Image>().sprite = _imageOfStars[stars];
            _levels[i].GetComponentInChildren<TextMeshProUGUI>().text = _levels[i].Level.ToString().Insert(_spaceBeforeNumerEng, " ");
        }

        LastLevel = (Level)(levelInArea - 1);
        Comic.FirstPartIsLoaded = false;
    }

    public void DestroyArea()
    {
        Destroy(gameObject);
    }

    public int FindFirstLevelInArea()
    {
        int areaNumber = (int)Area;
        int maxLevels = GameData.Maxlevels;
        return areaNumber * maxLevels - maxLevels + 1;
    }
    
}
