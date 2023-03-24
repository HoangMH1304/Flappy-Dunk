using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewIcon : MonoBehaviour
{
    [SerializeField] private Image ball, frontWing, backWing;
    void Start()
    {
        this.RegisterListener(EventID.OnChangeSkin, (param) => OnChangeSkin());
    }

    private void OnChangeSkin()
    {
        Debug.Log("Change preview icon skin");
        ball.sprite = ShopController.Instance.balls[PlayerPrefs.GetInt("BallIdSelected")].sprite;
        frontWing.sprite = ShopController.Instance.wings[PlayerPrefs.GetInt("WingIdSelected")].sprite;
        backWing.sprite = ShopController.Instance.wings[PlayerPrefs.GetInt("WingIdSelected")].sprite;
    }
}
