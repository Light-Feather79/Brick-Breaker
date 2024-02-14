using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static GameData Data;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _storyProgressText;

    private int _score;
    private int _coins;
    private float _storyProgress;
    
    private void Awake()
    {
        if (Data != null)
            Destroy(gameObject);
        else
            Data = this;

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

    private void Start()
    {
        // _scoreText.text = "Score: " + _score;
    }


    private void OnGetBrick(int points)
    {
        _score += points;
        _scoreText.text = "Score: " + _score;
    }

    private void OnGetCoin()
    {
        _coins++;
        _coinText.text = "Score: " + _coins;
    }

    public void GetAreaInfo(Area area)
    {

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
