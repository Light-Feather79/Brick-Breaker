using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
    public static event Action<int> BrickLifeCycle;

    [SerializeField] private AudioClip _breakSound;
    [SerializeField] private GameObject _blockSparklesVFX;
    [SerializeField] private Sprite[] _hitSprites;
    [SerializeField] private GameObject[] _bonusesPrefabs;
    [SerializeField] private BonusCoin _coinPrefab;
    
    private SpriteRenderer _spriteRenderer;
    private int _currentHits;
    private bool _coinBonus;

    private void Start()
    {
        BrickLifeCycle?.Invoke(0);
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

    public void SetCoin() => _coinBonus = true;

    private void DestroyBrick()
    {
        AudioSource.PlayClipAtPoint(_breakSound, Camera.main.transform.position);
        BrickLifeCycle?.Invoke(GetPointsForBrick());
        TriggerBlow();
        CreateRandomBonus();
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

    private void CreateRandomBonus()
    {
        if (_bonusesPrefabs.Length != 0 && _coinBonus == false)
        {
            int random = UnityEngine.Random.Range(0, 100);

            if (random < 65)
            {
                int randomBonus = UnityEngine.Random.Range(0, _bonusesPrefabs.Length);
                Instantiate(_bonusesPrefabs[randomBonus], transform.position, Quaternion.identity);
            }
        }
        
        else if (_coinBonus == true)
            Instantiate(_coinPrefab, transform.position, Quaternion.identity);

    }
}