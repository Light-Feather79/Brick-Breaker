using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField, Range(0.1f, 5f)] private float _gameSpeed = 1f;
    [SerializeField] private bool _isAutoPlay;
    
    private int _currentScore;
    [SerializeField] private int _ballCount;
    private Level _level;

    public bool IsAutoPlay { get => _isAutoPlay; }

    private void Awake()
    {
        _level = FindObjectOfType<Level>();

        _ballCount++;

        int gameStatusCount = FindObjectsOfType<GameSession>().Length;

        if (gameStatusCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        Brick.BrickDestroyed += AddToScore;
        Ball.BallLifeCycle += ManageBallCount;
    }

    private void OnDisable()
    {
        Brick.BrickDestroyed -= AddToScore;
        Ball.BallLifeCycle -= ManageBallCount;
    }

    private void Start()
    {
        _score.text = "Score: " + _currentScore;
    }

    private void Update()
    {
        Time.timeScale = _gameSpeed;
    }

    public void ResetGame() => Destroy(gameObject);

    public void OnGetCoin(int points) => AddToScore(points);

    private void AddToScore(int pointsForBrick)
    {
        _currentScore += pointsForBrick;
        _score.text = "Score: " + _currentScore;
    }

    private void ManageBallCount(bool isBallAdded, Ball ball)
    {
        if (isBallAdded)
            _ballCount++;
        else
            _ballCount--;

        CheckZeroBall();
    }

    private void CheckZeroBall()
    {
        if (_ballCount == 0 && _level.BreakableBricks > 0)
            SceneManager.LoadScene("Game over");
    }
}
