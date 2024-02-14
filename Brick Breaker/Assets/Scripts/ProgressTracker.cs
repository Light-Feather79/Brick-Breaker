using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressTracker : MonoBehaviour
{
    public static ProgressTracker Data;


    
    private void Awake()
    {
        if (Data != null)
            Destroy(gameObject);
        else
            Data = this;

        DontDestroyOnLoad(gameObject);
    }

}
