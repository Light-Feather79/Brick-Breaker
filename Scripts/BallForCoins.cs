using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallForCoins : BallBuyer
{
    [SerializeField] protected new Image _imageToBuy;
    [SerializeField] private TextMeshProUGUI _textPrice;
    [SerializeField] private int _price;

    private void Start()
    {
        if (GameData.Instance.BallPrefab.GetComponent<SpriteRenderer>().sprite == _ballImage.sprite)
        {
            GameData.Instance.ResetBallInfo(_ballImage.sprite, _isBought);
            _isBought = true;
            PickBall();
        }
        else
        {
            _isBought = GameData.Instance.GetBallInfo(_ballImage.sprite);

            if (_isBought == false)
            {
                _textPrice.text = _price.ToString();
                _imageToBuy.gameObject.SetActive(true);
            }
            else
            {
                _imageToBuy.gameObject.SetActive(false);
                ResetStatus();
            }
        }
    }

    public override void PickBall()
    {
        if (_isBought == false && _price <= GameData.Instance.YandexData.Coins)
        {
            _isBought = true;
            _imageToBuy.gameObject.SetActive(false);
            GameData.Instance.BuyBallByCoins(GameData.Instance.YandexData.Coins - _price);
            GameData.Instance.ResetBallInfo(_ballImage.sprite, _isBought);
            GameData.Instance.Upload();
        }

        if (_isBought)
        {
            GameData.Instance.SetBallSprite(_ballImage.sprite);
            _frame.SetActive(true);
            _imageToChoose.gameObject.SetActive(false);
            AnnounceBallPicked();
        }
    }
}
