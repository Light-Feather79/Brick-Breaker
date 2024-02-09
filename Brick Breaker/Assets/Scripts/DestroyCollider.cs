using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyCollider : MonoBehaviour
{
    private BallPooler _ballPooler;

    private void Awake()
    {
        _ballPooler = FindObjectOfType<BallPooler>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Ball>(out Ball ball))
            _ballPooler.Push(ball);
        else
            Destroy(other.gameObject);
    }
}
