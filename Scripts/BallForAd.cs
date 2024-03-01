using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallForAd : BallBuyer
{
    [SerializeField] private Image _imageToWatch;
    [SerializeField] private TextMeshProUGUI _textWatch;
    [SerializeField] private  int _maxAmountToWatch = 2;
    
    private  int _amountToWatch = 2;

    private void Start()
    {
        _amountToWatch = _maxAmountToWatch; 

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
                _textWatch.text = $"{_amountToWatch}/{_maxAmountToWatch}";
                _imageToWatch.gameObject.SetActive(true);
            }
            else
            {
                ResetStatus();
            }
        }
    }

    public override void PickBall()
    {
        if (_isBought == false)
        {
            if (_amountToWatch != 0)
                GameData.ShowRewardedAd();

            _textWatch.text = $"{--_amountToWatch}/{_maxAmountToWatch}";

            if (_amountToWatch == 0)
            {
                _imageToWatch.gameObject.SetActive(false);
                _isBought = true;
                GameData.Instance.ResetBallInfo(_ballImage.sprite, _isBought);
                GameData.Instance.Upload(); 
            }
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
