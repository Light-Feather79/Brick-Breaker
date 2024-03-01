using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private SoundSettings _soundSettings;

    public void OpenSettings()
    {
        _soundSettings.gameObject.SetActive(true);

        if (SceneManager.GetActiveScene().name != Level.StartMenu.ToString())
            Time.timeScale = 0f;
    }

    public void CloseSettings()
    {
        _soundSettings.gameObject.SetActive(false);

        if (SceneManager.GetActiveScene().name != Level.StartMenu.ToString())
            Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ContinueAfterAd()
    {
        GameData.ShowRewardedAd();
        
        FindObjectOfType<LevelManager>().ResetPuddle();
        GameData.Instance.AddBonusCoins();
        FindObjectOfType<LevelGameOverScreen>().gameObject.SetActive(false);
    }
}

