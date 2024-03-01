using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeaderInfo : MonoBehaviour
{
    private static HeaderInfo s_instance;

    [SerializeField] private TextMeshProUGUI _blackStarText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _storyProgressText;
    [SerializeField] private Image _progressBarFilled;

    private bool _isFirstFrame;

    private void Start()
    {
        if (s_instance != null)
            Destroy(gameObject);
        else
            s_instance = this;

        UpdateUI();
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        GameData.Instance.DataChanged += UpdateUI;
    }

    private void OnDisable()
    {
        GameData.Instance.DataChanged -= UpdateUI;
    }

    private void Update()
    {
        if (_isFirstFrame == false)
        {
            _isFirstFrame = true;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        _blackStarText.text = GameData.Instance.YandexData.BlackStars.ToString();
        _coinText.text = GameData.Instance.YandexData.Coins.ToString();
        _scoreText.text = "Score\n" + GameData.Instance.YandexData.Score;
        _storyProgressText.text = "Story progress: " + GameData.Instance.YandexData.StoryProgressPercent + "%";
        _progressBarFilled.fillAmount = GameData.Instance.YandexData.StoryProgressPercent / 100f;
        GameData.Instance.Upload();
    }
}

