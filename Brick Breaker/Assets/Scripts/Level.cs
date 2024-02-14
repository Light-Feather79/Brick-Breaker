using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private int _ballCount;
    [SerializeField] private int _coins;
    [SerializeField] private int _breakableBricks;

    private void OnEnable()
    {
        Ball.BallLifeCycle += TrackBallCount;
        Brick.BrickLifeCycle += TrackBricks;
        BonusCoin.CoinGained += OnGetCoin;
    }

    private void OnDisable()
    {
        Ball.BallLifeCycle -= TrackBallCount;
        Brick.BrickLifeCycle -= TrackBricks;
        BonusCoin.CoinGained -= OnGetCoin;
    }

    private void Start()
    {

    }

    private void TrackBricks(int points)
    {
        if (points == 0)
            _breakableBricks++;
        else
            _breakableBricks--;

        // if (_breakableBricks == 0)
        //     _sceneLoader.LoadNextScene();
    }

    private void TrackBallCount(bool isBallAdded)
    {
        if (isBallAdded)
            _ballCount++;
        else
            _ballCount--;

        if (_ballCount == 0 && _breakableBricks > 0)
            SceneManager.LoadScene("Game over");
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    private void OnGetCoin() => _coins++;

    public void Quit() => Application.Quit();
}
