using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
    public static event Action<int> BrickDestroyed;
    public static event Action BrickCreated;

    [SerializeField] AudioClip _breakSound;
    [SerializeField] GameObject _blockSparklesVFX;
    [SerializeField] private Sprite[] _hitSprites;
    
    private SpriteRenderer _spriteRenderer;
    private int _currentHits;

    private void Start()
    {
        BrickCreated?.Invoke();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        int index = _currentHits;
        _currentHits++;

        if (_currentHits <= _hitSprites.Length)
        {
            if (_hitSprites[index] != null)
                _spriteRenderer.sprite = _hitSprites[index];    
        }
        else
        {
            DestroyBrick();
        }
    }

    protected void DestroyBrick()
    {
        AudioSource.PlayClipAtPoint(_breakSound, Camera.main.transform.position);
        BrickDestroyed?.Invoke(GetPointsForBrick());
        TriggerBlow();
        Destroy(gameObject);
    }

    private int GetPointsForBrick()
    {
        if(_hitSprites.Length == 0)
            return 67;
        if(_hitSprites.Length == 1)
            return 91;
        if(_hitSprites.Length == 2)
            return 119;
        else
            return 144;
    }

    private void TriggerBlow() => Destroy(Instantiate(_blockSparklesVFX, transform.position, Quaternion.identity).gameObject, 1f);
}