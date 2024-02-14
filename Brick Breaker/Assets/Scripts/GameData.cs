using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static GameData Instance;
    public static event Action<int> DataChanged;

    private Dictionary<Level, int> _areaProgress;

    public int Score { get; private set; }
    public int Coins { get; private set; }
    public float StoryProgress { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        _areaProgress = new Dictionary<Level, int>();
 
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        Brick.BrickLifeCycle += OnGetBrick;
        BonusCoin.CoinGained += OnGetCoin;
    }

    private void OnDisable()
    {
        Brick.BrickLifeCycle -= OnGetBrick;
        BonusCoin.CoinGained -= OnGetCoin;
    }

    private void OnGetBrick(int points)
    {
        Score += points;
        DataChanged?.Invoke(points);
    }

    private void OnGetCoin()
    {
        Coins++;
        DataChanged?.Invoke(1);
    }

    public int GetAreaInfo(Level level)
    {
        if (_areaProgress.ContainsKey(level) == false)
            _areaProgress[level] = 0;
        
        return _areaProgress[level];
    }

    public void ResetLevelData(int levelProgress, Level level)
    {
        if (_areaProgress.ContainsKey(level) == false || _areaProgress[level] < levelProgress)
            _areaProgress[level] = levelProgress;
    }
}

public enum Area
{
    Area1,
    Area2,
    Area3,
    Area4,
    Area5,
}

public enum Level
{
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
    Level6,
    Level7,
    Level8,
    Level9,
    Level10,
    Level11,
    Level12,
    Level13,
    Level14,
    Level15,
    StartMenu,
}
