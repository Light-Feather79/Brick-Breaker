using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Level _level;

    public Level Level { get => _level;}

    public void LoadLevel()
    {
        SceneManager.LoadScene(Level.ToString());
    }
}
