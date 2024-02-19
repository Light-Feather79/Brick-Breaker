using UnityEngine;
using System.Collections.Generic;

public class BallPooler : MonoBehaviour
{
    [SerializeField] private int _poolSize = 20;
    [SerializeField] private int _currentPoolSize = 20;

    private Queue<Ball> s_ballPool;
    private Ball _ballPrefab;

    void Awake()
    {
        _ballPrefab = GameData.Instance._ballPrefab;

        s_ballPool = new Queue<Ball>();
        Queue<Ball> ballPool = new Queue<Ball>();

        for (int i = 0; i < _poolSize; i++)
            ballPool.Enqueue(Pull());

        for (int i = 0; i < _poolSize; i++)
            Push(ballPool.Dequeue());
    }

    public Ball Pull()
    {
        _currentPoolSize--;

        if (s_ballPool.Count == 0)
        {
            Ball newBall = Instantiate(_ballPrefab);
            newBall.gameObject.SetActive(true);
            return newBall;
        }

        Ball ball = s_ballPool.Dequeue();
        ball.gameObject.SetActive(true);
        return ball;
    }

    public void Push(Ball ball)
    {
        ball.gameObject.SetActive(false);
        s_ballPool.Enqueue(ball);
        _currentPoolSize++;
    }
}

