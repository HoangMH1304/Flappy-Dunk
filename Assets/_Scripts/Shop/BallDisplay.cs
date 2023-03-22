using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallDisplay : MonoBehaviour
{
    private Ball ball;
    [SerializeField] private Image ballSprite;

    public void Show(Ball _item)
    {
        ball = _item;
        ballSprite.sprite = ball.sprite;
    }
}
