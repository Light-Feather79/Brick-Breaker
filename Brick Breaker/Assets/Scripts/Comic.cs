using System;
using UnityEngine;
using UnityEngine.UI;

public class Comic : MonoBehaviour
{

    [SerializeField] private ComicSO _comic;
    [SerializeField] private Image _pages;
    [SerializeField] private Image _lock;
    [SerializeField] private bool _isStartingComic;

    public static bool FirstPartIsLoaded;
    private Level _level;

    private void Start()
    {

        AreaSetter areaSetter = FindObjectOfType<AreaSetter>();
        int firstLevelInArea = areaSetter.FindFirstLevelInArea();

        if (firstLevelInArea == 1 && FirstPartIsLoaded == false)
        {
            FirstPartIsLoaded = true;
            _comic = areaSetter._allComicParts[0];
            _level = Level.Level1;
            return;
        }

        SetLevel(areaSetter, firstLevelInArea);

        if (GameData.Instance.GetStarAmountOfLevel(_level) == 0)
            _lock.gameObject.SetActive(true);

    }

    private void SetFirstPage()
    {
        _pages.sprite = _comic.GetFirstPage();
        _comic.FirstTimeOpened = false;
    }

    private void SetLevel(AreaSetter areaSetter, int firstLevelInArea)
    {
        int numberOfPart = (int)areaSetter.Area * 2 - 1;

        if (_isStartingComic)
        {
            _comic = areaSetter._allComicParts[numberOfPart - 1];
            _level = (Level)firstLevelInArea - 1;
        }
        else
        {
            _comic = areaSetter._allComicParts[numberOfPart];
            _level = (Level)(firstLevelInArea + GameData.Maxlevels - 1);
        }
    }

    public void GoToNextPage() => _pages.sprite = _comic.NextPage(1);

    public void GoToPreviousPage() => _pages.sprite = _comic.NextPage(-1);

    public void OpenComic()
    {
        if (_level != Level.Level1 && GameData.Instance.GetStarAmountOfLevel(_level) == 0)
            return;

        _lock.gameObject.SetActive(false);
        _pages.gameObject.SetActive(true);
        SetFirstPage();
    }
}
