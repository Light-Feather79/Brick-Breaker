using System;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public const int MaxStars = 4;
    public const int Maxlevels = 6;
    public const int MaxCoinsForLevel = 7;

    public static GameData Instance;
    
    public event Action DataChanged;

    public Ball _ballPrefab;

    private Dictionary<Level, int> _areaProgress;
    private Dictionary<Sprite, bool> _boughtBalls;

    public int Score { get; private set; }
    public int Coins { get; private set; }
    public int BlackStars { get; private set; }
    public float StoryProgress { get; private set; }
    public float MusicValue { get; private set; }
    public float SFXValue { get; private set; }
    public bool IsGameComplited { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        _areaProgress = new Dictionary<Level, int>();
        _boughtBalls = new Dictionary<Sprite, bool>();
 
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        Brick.BrickLifeCycle += OnGetBrick;
        BonusCoin.CoinGained += OnGetCoin;
        BonusBlackStar.BlackStarGained += OnGetBlackStar;
        SoundSettings.SoundSettingsChanged += SetSoundSettings;
    }

    private void OnDisable()
    {
        Brick.BrickLifeCycle -= OnGetBrick;
        BonusCoin.CoinGained -= OnGetCoin;
        BonusBlackStar.BlackStarGained -= OnGetBlackStar;
        SoundSettings.SoundSettingsChanged -= SetSoundSettings;
    }

    public void AddBonusCoins()
    {
        Coins += 2;
        DataChanged?.Invoke();
    }

    public void BuyBallByCoins(int coins)
    {
        Coins = coins;
        DataChanged?.Invoke();
    }

    public void BuyBallByStars(int coins)
    {
        BlackStars = coins;
        DataChanged?.Invoke();
    }

    public void SetBallSprite(Sprite sprite) => _ballPrefab.GetComponent<SpriteRenderer>().sprite = sprite;

    public bool GetBallInfo(Sprite sprite) => _boughtBalls[sprite] = _boughtBalls.ContainsKey(sprite) ? _boughtBalls[sprite] : false;

    public void ResetBallInfo(Sprite sprite, bool isBought) => _boughtBalls[sprite] = isBought;

    public int GetStarAmountOfLevel(Level level)
    {
        if (_areaProgress.ContainsKey(level) == false)
            _areaProgress[level] = 0;
        
        return _areaProgress[level];
    }

    public void ResetLevelData(int levelProgress, Level level)
    {
        if (_areaProgress.ContainsKey(level) == false || _areaProgress[level] < levelProgress)
        {
            _areaProgress[level] = levelProgress;
            UpdateProgress();
            DataChanged?.Invoke();
        
            if ((int)level == Enum.GetNames(typeof(Level)).Length - 1)
                IsGameComplited = true;
        }
    }

    private void OnGetBrick(int points)
    {
        Score += points;
        DataChanged?.Invoke();
    }

    private void OnGetCoin()
    {
        Coins++;
        DataChanged?.Invoke();
    }

    private void OnGetBlackStar()
    {
        BlackStars++;
        DataChanged?.Invoke();
    }

    private void UpdateProgress()
    {
        int levelAmount = Enum.GetNames(typeof(Level)).Length - 1;
        float maxProgress = (float)(levelAmount * MaxStars);
        float currentProgress = 0;

        foreach (int areaProgress in _areaProgress.Values)
            currentProgress += areaProgress;

        StoryProgress = (float)Math.Round(currentProgress / maxProgress * 100f, 2);
    }

    private void SetSoundSettings(float volume, Sound sound)
    {
        switch (sound)
        {
            case Sound.Music:
                MusicValue = volume;
                break;

            case Sound.SFX:
                SFXValue = volume;
                break;
        }
    }
}





public enum Area
{
    Area1 = 1,
    Area2,
    Area3,
    Area4,
    Area5,
}

public enum Level
{
    Level1 = 1,
    Level2,
    Level3,
    Level4,
    Level5,
    Level6,
    // Level7,
    // Level8,
    // Level9,
    // Level10,
    // Level11,
    // Level12,
    // Level13,
    // Level14,
    // Level15,
    StartMenu,
}
