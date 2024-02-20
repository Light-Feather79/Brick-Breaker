using UnityEngine;
using System;
using Unity.Mathematics;
using UnityEngine.UI;
using TMPro;

public class BallPicker : MonoBehaviour
{
    private static event Action<BallPicker> BallBicked;

    [SerializeField] private TextMeshProUGUI _textPrice;
    [SerializeField] private Image _imageToBuy;
    [SerializeField] private bool _toWatch;

    [SerializeField] private TextMeshProUGUI _textWatch;
    [SerializeField] private Image _imageToWatch;
    
    [SerializeField] private GameObject _frame;
    [SerializeField] private Image _imageToChoose;
    [SerializeField] private Image _ballImage;
    [SerializeField] private int _price;

    private bool _isBought;
    private int _amountToWatch = 2;
    private int _maxAmountToWatch = 2;

    private void Start()
    {
        if (GameData.Instance._ballPrefab.GetComponent<SpriteRenderer>().sprite == _ballImage.sprite)
        {
            _isBought = true;
            GameData.Instance.ResetBallInfo(_ballImage.sprite, _isBought);
            PickBall();
        }
        else
        {
            _isBought = GameData.Instance.GetBallInfo(_ballImage.sprite);

            if (_isBought == false)
            {
                if (_toWatch)
                {
                    _textWatch.text = $"{_amountToWatch}/{_maxAmountToWatch}";
                    _imageToWatch.gameObject.SetActive(true);
                }
                else
                {
                    _textPrice.text = _price.ToString();
                    _imageToBuy.gameObject.SetActive(true);
                }
            }
            else
            {
                ResetStatus();
            }
        }
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
        if (_toWatch)
        {
            _textWatch.text = $"{--_amountToWatch}/{_maxAmountToWatch}";
            
            if (_amountToWatch == 0)
            {
                _imageToWatch.gameObject.SetActive(false);
                _toWatch = false;
                _isBought = true;
                GameData.Instance.ResetBallInfo(_ballImage.sprite, _isBought);
            }
        }

        if (_isBought == false && _price <= GameData.Instance.Coins)
        {
            _isBought = true;
            _imageToBuy.gameObject.SetActive(false);
            GameData.Instance.BuyBall(GameData.Instance.Coins - _price);
            GameData.Instance.ResetBallInfo(_ballImage.sprite, _isBought);
        }

        if (_isBought)
        {
            GameData.Instance.SetBallSprite(_ballImage.sprite);
            _frame.SetActive(true);
            _imageToChoose.gameObject.SetActive(false);
            BallBicked?.Invoke(this);
        }
    }

    private void OnBallPicked(BallPicker ballPicker)
    {
        if (ballPicker == this || _isBought == false)
            return;

        ResetStatus();
    }

    private void ResetStatus()
    {
        _frame.SetActive(false);
        _imageToBuy.gameObject.SetActive(false);
        _imageToChoose.gameObject.SetActive(true);
    }
}
