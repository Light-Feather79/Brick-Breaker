using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeaderInfo : MonoBehaviour
{
    private static HeaderInfo s_instance;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _storyProgressText;
    [SerializeField] private Image _progressBarFilled;

    private void Awake()
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

    private void UpdateUI()
    {
        _scoreText.text = "Score: " + GameData.Instance.Score;
        _coinText.text = "Coins: " + GameData.Instance.Coins;
        _storyProgressText.text = "Story progress: " + GameData.Instance.StoryProgress + "%";
        _progressBarFilled.fillAmount = GameData.Instance.StoryProgress / 100f;
    }
}

