using UnityEngine;
using System;
using Unity.Mathematics;
using UnityEngine.UI;

public class BallPicker : MonoBehaviour
{
    private static event Action<BallPicker> BallBicked;

    [SerializeField] private GameObject _frame;
    [SerializeField] private Image _image;


    private void Start()
    {
        if (GameData.Instance._ballPrefab.GetComponent<SpriteRenderer>().sprite == _image.sprite)
            PickBall();
    }

    private void OnEnable()
    {
        BallBicked += OnBallPicked;
    }
    
    private void OnDisable()
    {
        BallBicked -= OnBallPicked;
    }

    public void PickBall()
    {
        _frame.SetActive(true);
        GameData.Instance.SetBallSprite(_image.sprite);
        BallBicked?.Invoke(this);
    }

    private void OnBallPicked(BallPicker ballPicker)
    {
        if (ballPicker != this)
        {
            _frame.SetActive(false);
        }
    }
}
