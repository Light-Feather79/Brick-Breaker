using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeaderInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _storyProgressText;

    private int _score;
    private int _coins;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == Level.StartMenu.ToString())
        {
            _scoreText.text = "Score: " + GameData.Instance.Score;
            _coinText.text = "Coins: " + GameData.Instance.Coins;
        }
        else
        {
            _scoreText.text = "Score: " + 0;
            _coinText.text = "Coins: " + 0;
        }
    }

    private void OnEnable() => GameData.DataChanged += UpdateUI;

    private void OnDisable() => GameData.DataChanged -= UpdateUI;

    private void UpdateUI(int points)
    {
        if (points == 1)
            _coinText.text = "Coins: " + ++_coins;
        else
            _scoreText.text = "Score: " + (_score += points);
    }
}

