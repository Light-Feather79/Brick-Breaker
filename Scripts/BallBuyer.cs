using UnityEngine;
using System;
using Unity.Mathematics;
using UnityEngine.UI;
using TMPro;

public class BallBuyer : MonoBehaviour
{
    private static event Action<BallBuyer> BallPicked;

    [SerializeField] protected Image _ballImage;
    [SerializeField] protected GameObject _frame;
    [SerializeField] protected Image _imageToChoose;

    protected Image _imageToBuy;
    protected bool _isBought;

    private void OnEnable()
    {
        BallPicked += OnBallPicked;
    }
    
    private void OnDisable()
    {
        BallPicked -= OnBallPicked;
    }

    public virtual void PickBall(){}

    private void OnBallPicked(BallBuyer ballBuyer)
    {
        if (ballBuyer == this || _isBought == false)
            return;

        ResetStatus();
    }

    protected void AnnounceBallPicked() => BallPicked?.Invoke(this);

    protected void ResetStatus()
    {
        _frame.SetActive(false);
        _imageToChoose.gameObject.SetActive(true);
    }
}
