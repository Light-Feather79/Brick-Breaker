using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField, Range(0.1f, 5f)] private float _gameSpeed = 1f;
    [SerializeField] private int _currentScore = 0;
    [SerializeField] private TextMeshProUGUI _score;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;

        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable() => Brick.BrickDestroyed += AddToScore;

    private void OnDisable() => Brick.BrickDestroyed -= AddToScore;

    private void Start()
    {
        _score.text = "Score: " + _currentScore;
    }

    private void Update()
    {
        Time.timeScale = _gameSpeed;
    }

    private void AddToScore(int pointsForBrick)
    {
        _currentScore += pointsForBrick;
        _score.text = "Score: " + _currentScore;
    }

    public void ResetGame() => Destroy(gameObject);
}
