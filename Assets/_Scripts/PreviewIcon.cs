using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PreviewIcon : MonoBehaviour
{
    [SerializeField] private Image ball, frontWing, backWing;
    private RectTransform rectTransform;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        this.RegisterListener(EventID.OnChangeSkin, (param) => OnChangeSkin());
    }

    // private void OnEnable() 
    // {
    //     OnChangeSkin();
    // }

    private void OnChangeSkin()
    {
        ball.sprite = ShopController.Instance.balls[PlayerPrefs.GetInt("BallIdSelected")].sprite;
        frontWing.sprite = ShopController.Instance.wings[PlayerPrefs.GetInt("WingIdSelected")].sprite;
        backWing.sprite = ShopController.Instance.wings[PlayerPrefs.GetInt("WingIdSelected")].sprite;
        // rectTransform.DOScale(1, 0.5f).SetEase(Ease.InOutSine).SetUpdate(true);
    }
}
