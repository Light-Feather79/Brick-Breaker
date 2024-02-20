using UnityEngine;
using System;
using Unity.Mathematics;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Comic : MonoBehaviour
{
    [SerializeField] private ComicSO _comic;
    [SerializeField] private Image _pages;

    private void OnEnable()
    {
        _pages.sprite = _comic.GetFirstPage();
        _comic.IsOpen = false;
    }

    public void GoToNextPage() => _pages.sprite = _comic.NextPage(1);

    public void GoToPreviousPage() => _pages.sprite = _comic.NextPage(-1);

    public void OpenComic()
    {
        _pages.gameObject.SetActive(true);
    }
}
