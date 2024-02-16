using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    [SerializeField] private InfoPanel _infoPanel;

    public void OpenInfo()
    {
        _infoPanel.gameObject.SetActive(true);
    }

    public void CloseInfo()
    {
        _infoPanel.gameObject.SetActive(false);
    }
}

