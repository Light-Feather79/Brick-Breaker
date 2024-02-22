using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Comic")]
public class ComicSO : ScriptableObject
{
    [SerializeField] private int _part;
    [SerializeField] private Sprite[] _pages;

    public bool FirstTimeOpened;
    private int _currentPage = 0;

    public int Part => _part;

    public Sprite GetFirstPage() => _pages[0];

    public Sprite NextPage(int nextPage)
    {
        if (FirstTimeOpened == false)
        {
            FirstTimeOpened = true;
            _currentPage = 0;
        }

        _currentPage += nextPage;

        if (_currentPage < 0)
            _currentPage++;
        else if(_currentPage == _pages.Length)
            _currentPage--;

        return _pages[_currentPage];
    }
}
