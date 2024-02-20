using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Level Level;

    [SerializeField] private Image _lockImage;

    private bool _levelIsOpen;

    private void Start()
    {
        int previousLevel = (int)Level - 1;

        if (Level == Level.Level1 || GameData.Instance.GetStarAmountOfLevel((Level)previousLevel) > 0)
            _levelIsOpen = true;
        else
            _lockImage.gameObject.SetActive(true);
    }

    public void LoadLevel()
    {
        if (_levelIsOpen)
            SceneManager.LoadScene(Level.ToString());
    }
}

