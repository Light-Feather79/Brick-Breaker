using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCamera : MonoBehaviour
{
    private void Start() => FindObjectOfType<HeaderInfo>().GetComponent<Canvas>().worldCamera = Camera.main;
}
