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
    [SerializeField] private int _maxCoins = 5;
    [SerializeField] private int _coins = 0;

    private bool _death;
    private bool _blackStar;

    private void OnEnable()
    {
        Ball.BallLifeCycle += TrackBallCount;
        Brick.BrickLifeCycle += TrackBricks;
        BonusCoin.CoinGained += OnCoinGained;
    }

    private void OnDisable()
    {
        Ball.BallLifeCycle -= TrackBallCount;
        Brick.BrickLifeCycle -= TrackBricks;
        BonusCoin.CoinGained -= OnCoinGained;
    }

    private void Start()
    {
        GiveCoinsToRandomBricks();
    }

    private void GiveCoinsToRandomBricks()
    {
        List<Brick> bricks = FindObjectsOfType<Brick>().ToList();
        int setCoins = 0;

        while(setCoins < _maxCoins)
        {
            for (int i = bricks.Count - 1; i >= 0; i--)
            {
                int coinChance = UnityEngine.Random.Range(0, 100);
                Debug.Log(bricks.Count);

                if (coinChance < 50)
                {
                    bricks[i].SetCoin();
                    bricks.RemoveAt(i);
                    setCoins++;
                    Debug.Log(setCoins);
                }

                if (setCoins == _maxCoins)
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

        if (_breakableBricks == 0)
            WinGame();
    }

    private void TrackBallCount(bool isBallAdded)
    {
        if (isBallAdded)
            _ballCount++;
        else
            _ballCount--;

        if (_ballCount == 0 && _breakableBricks > 0)
            LoseGame();
    }

    private void OnCoinGained() => _coins++;

    private void WinGame()
    {
        int levelProgress = 1;
        Enum.TryParse(SceneManager.GetActiveScene().name, out Level level);

        levelProgress = _coins == _maxCoins ? levelProgress + 1 : levelProgress;
        levelProgress = _death == false ? levelProgress + 1 : levelProgress;
        levelProgress = _blackStar == true ? levelProgress + 1 : levelProgress;

        GameData.Instance.ResetLevelData(levelProgress, level);
        
        StartCoroutine(WidenYRoutine());
    }

    private IEnumerator WidenYRoutine()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);

    }

    public void LoseGame()
    {



        SceneManager.LoadScene(0);
        // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        // {
        //     SceneManager.LoadScene(0);
        // }
        // else
        // {
        //     SceneManager.LoadScene(currentSceneIndex + 1);
        // }
    }
}
