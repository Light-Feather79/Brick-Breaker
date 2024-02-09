using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
public class Level : MonoBehaviour
{
    [SerializeField] private int _breakableBricks;

    private SceneLoader _sceneLoader;

    public int BreakableBricks { get => _breakableBricks; }

    private void OnEnable()
    {
        Brick.BrickDestroyed += DeductDestroyedBrick;
        Brick.BrickCreated += CountBreakableBricks;
    }

    private void OnDisable()
    {
        Brick.BrickDestroyed -= DeductDestroyedBrick;
        Brick.BrickCreated -= CountBreakableBricks;
    }

    private void Start()
    {
        _sceneLoader = GetComponent<SceneLoader>();
    }

    private void CountBreakableBricks() => _breakableBricks++;

    private void DeductDestroyedBrick(int i)
    {
        _breakableBricks--;

        if (_breakableBricks == 0)
            _sceneLoader.LoadNextScene();
    }
}
