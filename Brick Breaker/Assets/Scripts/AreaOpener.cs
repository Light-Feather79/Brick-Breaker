using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AreaOpener : MonoBehaviour
{
    [SerializeField] private Area _areaTag;
    [SerializeField] private AreaSetter _area;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        gameObject.tag = _areaTag.ToString(); 
        _text.text = _areaTag.ToString(); 
    }

    public void OpenArea()
    {
        Transform area = Instantiate(_area).transform;
        area.tag = _areaTag.ToString();
        area.transform.SetParent(GameObject.Find("Canvas").transform, false);
    }

}
