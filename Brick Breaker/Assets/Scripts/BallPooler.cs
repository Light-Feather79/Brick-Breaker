using UnityEngine;
using System.Collections.Generic;

public class BallPooler : MonoBehaviour
{
    [SerializeField] private int _currentPoolSize = 20;

    private Queue<Ball> _ballPool;
    private Ball _ballPrefab;

    void Awake()
    {
        _ballPrefab = GameData.Instance.YandexData.BallPrefab;

        _ballPool = new Queue<Ball>();
        int poolSize = _currentPoolSize;
        Queue<Ball> ballPool = new Queue<Ball>();

        for (int i = 0; i < poolSize; i++)
            ballPool.Enqueue(Pull());

        for (int i = 0; i < poolSize; i++)
            Push(ballPool.Dequeue());
    }

    public Ball Pull()
    {
        _currentPoolSize--;

        if (_ballPool.Count == 0)
        {
            Ball newBall = Instantiate(_ballPrefab);
            newBall.gameObject.SetActive(true);
            return newBall;
        }

        Ball ball = _ballPool.Dequeue();
        ball.gameObject.SetActive(true);
        return ball;
    }

    public void Push(Ball ball)
    {
        ball.gameObject.SetActive(false);
        _ballPool.Enqueue(ball);
        _currentPoolSize++;
    }
}

