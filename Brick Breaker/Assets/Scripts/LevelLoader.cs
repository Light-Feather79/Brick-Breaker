using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Level Level;

    [SerializeField] private GameObject _levelNotComplitedWarning;

    public void LoadLevel()
    {
        int previousLevel = (int)Level;
        previousLevel--;

        if (GameData.Instance.GetStarAmountOfLevel((Level)previousLevel) > 0 || Level == Level.Level1)
            SceneManager.LoadScene(Level.ToString());
        else
            _levelNotComplitedWarning.SetActive(true);
            
    }
}
