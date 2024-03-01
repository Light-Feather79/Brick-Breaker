using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    
        
    private void Start() => GameData.Instance.Upload();




    [DllImport("__Internal")]
    private static extern void RateGame();



    public void UnityRateGame() => RateGame();
}