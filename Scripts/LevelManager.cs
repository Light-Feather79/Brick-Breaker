using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _ballCount;
    [SerializeField] private int _breakableBricks;
    [SerializeField] private int _coins;
    [SerializeField] private LevelGameOverScreen _levelGameOverScreen;
    [SerializeField] private LevelWinScreen _levelWinScreen;
    [SerializeField] private Paddle _paddlePrefab;

    private bool _death;
    private bool _blackStar;
    private bool _isPaused;
    private Paddle _paddle;

    public int BallCount => _ballCount;

    private void OnEnable()
    {
        Ball.BallLifeCycle += TrackBallCount;
        Brick.BrickLifeCycle += TrackBricks;
        BonusCoin.CoinGained += OnCoinGained;
        BonusBlackStar.BlackStarGained += OnBlackStarGained;
    }

    private void OnDisable()
    {
        Ball.BallLifeCycle -= TrackBallCount;
        Brick.BrickLifeCycle -= TrackBricks;
        BonusCoin.CoinGained -= OnCoinGained;
        BonusBlackStar.BlackStarGained -= OnBlackStarGained;
    }

    private void Start()
    {
        FindObjectOfType<HeaderInfo>().GetComponent<Canvas>().worldCamera = Camera.main;

        Enum.TryParse(SceneManager.GetActiveScene().name, out Level level);

        if ((int)level % 3 == 0)
            GameData.ShowAd();

        GiveCoinsToRandomBricks();
        _paddle = Instantiate(_paddlePrefab);
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }

    private void OnCoinGained() => _coins++;

    private void OnBlackStarGained() => _blackStar = true;

    private void GiveCoinsToRandomBricks()
    {
        List<Brick> bricks = FindObjectsOfType<Brick>().ToList();
        int setCoins = 0;
        bool blackStartIsSet = false;

        while(setCoins < GameData.MaxCoinsForLevel)
        {
            for (int i = bricks.Count - 1; i >= 0; i--)
            {
                int coinChance = UnityEngine.Random.Range(0, 100);

                if (GameData.Instance.YandexData.IsGameComplited && blackStartIsSet == false)
                {
                    bricks[i].BlackStarBonus = true;
                    blackStartIsSet = true;
                    continue;
                }

                if (coinChance < 50)
                {
                    bricks[i].SetCoin();
                    bricks.RemoveAt(i);
                    setCoins++;
                }

                if (setCoins == GameData.MaxCoinsForLevel)
                    return;
            }
        }
    }

    private void TrackBricks(int points)
    {
        if (points == 0)
            _breakableBricks++;
        else
            _breakableBricks--;

        if (_breakableBricks == 0 && Time.timeScale != 0)
            StartCoroutine(EndLevel());
    }

    private void TrackBallCount(bool isBallAdded)
    {
        if (isBallAdded)
            _ballCount++;
        else
            _ballCount--;

        if (_isPaused)
            return;

        if (_ballCount == 0 && _breakableBricks > 0 && Time.timeScale != 0)
            LoseGame();
    }

    private IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(2.5f);
        WinGame();
    }

    private void WinGame()
    {
        int starsEarned = 1;
        Enum.TryParse(SceneManager.GetActiveScene().name, out Level level);

        starsEarned = _coins == GameData.MaxCoinsForLevel ? starsEarned + 1 : starsEarned;
        starsEarned = _death == false ? starsEarned + 1 : starsEarned;
        starsEarned = _blackStar == true ? starsEarned + 1 : starsEarned;

        GameData.Instance.ResetLevelData(starsEarned, level);
        
        // if ((int)level == Enum.GetNames(typeof(Level)).Length - 1)
        //     FindObjectOfType<Yandex>().gameObject.SetActive(true);
        // else
            _levelWinScreen.gameObject.SetActive(true);
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        _death = true;
        _levelGameOverScreen.gameObject.SetActive(true);
    }

    public void ResetPuddle()
    {
        Destroy(_paddle.gameObject);
        _paddle = Instantiate(_paddlePrefab);
        Time.timeScale = 1f;
    }
}
