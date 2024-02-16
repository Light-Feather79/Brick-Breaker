using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AreaOpener : MonoBehaviour
{
    [SerializeField] private Area _area;
    [SerializeField] private AreaSetter _levelsOfArea;
    [SerializeField] private TextMeshProUGUI _text;

    private int _spaceBeforeNumиerEng = 4;

    private void Start()
    {
        _text.text = _area.ToString().Insert(_spaceBeforeNumиerEng, " "); 
    }

    public void OpenArea()
    {
        AreaSetter area = Instantiate(_levelsOfArea).GetComponent<AreaSetter>();
        area.Area = _area;
        area.transform.SetParent(GameObject.Find("CanvasMenu").transform, false);
    }

}
