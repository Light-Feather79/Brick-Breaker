using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Level Level;

    public void LoadLevel()
    {
        SceneManager.LoadScene(Level.ToString());
    }
}
