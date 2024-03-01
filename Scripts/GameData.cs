using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[Serializable]
public class YandexData
{   
    public int Score;
    public int Coins;
    public int BlackStars;
    public float StoryProgressPercent;
    public float MusicValue;
    public float SFXValue;
    public bool IsGameComplited;


}

public class GameData : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);
    [DllImport("__Internal")]
    private static extern void LoadExtern();
    // [DllImport("__Internal")]
    // private static extern void LBScore(int value);
    // [DllImport("__Internal")]
    // private static extern void LBCoins(int value);
    // [DllImport("__Internal")]
    // private static extern void LBBlackStars(int value);
    // [DllImport("__Internal")]
    // private static extern void LBProgress(float value);
    [DllImport("__Internal")]
    public static extern void ShowAd();
    [DllImport("__Internal")]
    public static extern void ShowRewardedAd();

    public void Upload()
    {
        string jsonString = JsonUtility.ToJson(YandexData);
        SaveExtern(jsonString);
    }

    public void Download(string value)
    {
        YandexData = JsonUtility.FromJson<YandexData>(value);
    }

    // public void SetLeaderboardInfo()
    // {
    //     LBScore(YandexData.Score);
    //     LBCoins(YandexData.Coins);
    //     LBBlackStars(YandexData.BlackStars);
    //     LBProgress(YandexData.StoryProgressPercent);
    // }






    public Ball BallPrefab;
    public Dictionary<Level, int> ProgressOfLevels;
    public Dictionary<Sprite, bool> BoughtBalls;


    public const int MaxStars = 4;
    public const int Maxlevels = 6;
    public const int MaxCoinsForLevel = 7;

    public static GameData Instance;
    public YandexData YandexData;
    
    public event Action DataChanged;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
 
        ProgressOfLevels = new Dictionary<Level, int>();
        BoughtBalls = new Dictionary<Sprite, bool>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // #if !UNITY_EDITOR && UNITY_WEBGL


            LoadExtern();
            ShowAd();
        // #endif
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
        YandexData.Coins += 2;
        DataChanged?.Invoke();
    }

    public void BuyBallByCoins(int coins)
    {
        YandexData.Coins = coins;
        DataChanged?.Invoke();
    }

    public void BuyBallByStars(int coins)
    {
        YandexData.BlackStars = coins;
        DataChanged?.Invoke();
    }

    public void SetBallSprite(Sprite sprite) => BallPrefab.GetComponent<SpriteRenderer>().sprite = sprite;

    public bool GetBallInfo(Sprite sprite) => BoughtBalls[sprite]
        = BoughtBalls.ContainsKey(sprite) ? BoughtBalls[sprite] : false;

    public void ResetBallInfo(Sprite sprite, bool isBought) => BoughtBalls[sprite] = isBought;

    public int GetStarAmountOfLevel(Level level)
    {
        if (ProgressOfLevels.ContainsKey(level) == false)
            ProgressOfLevels[level] = 0;
        
        return ProgressOfLevels[level];
    }

    public void ResetLevelData(int levelProgress, Level level)
    {
        if (ProgressOfLevels.ContainsKey(level) == false || ProgressOfLevels[level] < levelProgress)
        {
            if ((int)level == Enum.GetNames(typeof(Level)).Length - 1)
                YandexData.IsGameComplited = true;

            ProgressOfLevels[level] = levelProgress;
            UpdateProgress();
            DataChanged?.Invoke();

            // #if !UNITY_EDITOR && UNITY_WEBGL
                Upload();
                // SetLeaderboardInfo();
            // #endif
        }
    }

    private void OnGetBrick(int points)
    {
        YandexData.Score += points;
        DataChanged?.Invoke();
    }

    private void OnGetCoin()
    {
        YandexData.Coins++;
        DataChanged?.Invoke();
    }

    private void OnGetBlackStar()
    {
        YandexData.BlackStars++;
        DataChanged?.Invoke();
    }

    private void UpdateProgress()
    {
        int levelAmount = Enum.GetNames(typeof(Level)).Length - 1;
        float maxProgress = (float)(levelAmount * MaxStars);
        float currentProgress = 0;

        foreach (int areaProgress in ProgressOfLevels.Values)
            currentProgress += areaProgress;

        YandexData.StoryProgressPercent = (float)Math.Round(currentProgress / maxProgress * 100f, 2);
    }

    private void SetSoundSettings(float volume, Sound sound)
    {
        switch (sound)
        {
            case Sound.Music:
                YandexData.MusicValue = volume;
                break;

            case Sound.SFX:
                YandexData.SFXValue = volume;
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
