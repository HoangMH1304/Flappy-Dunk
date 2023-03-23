using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallDisplay : MonoBehaviour
{
    private Ball ball;
    [SerializeField] private Image ballSprite;
    [SerializeField] private GameObject mark, lockOverlay;

    public void Show(Ball _item)
    {
        ball = _item;
        ballSprite.sprite = ball.sprite;
        //setactive mark icon or lock icon
        if (PlayerPrefs.GetInt(_item.type.ToString() + "IdSelected") == _item.id)
        {
            mark.SetActive(true);
        }
        if(PlayerPrefManager.Instance.IsUnlockItem(ball.type, ball.id))
        {
            lockOverlay.SetActive(false);
        }
        else
        {
            lockOverlay.SetActive(true);
        }
    }

    public void OnClick()
    {
        if(PlayerPrefManager.Instance.IsUnlockItem(ball.type, ball.id))
        {


            mark.SetActive(true);
        }
    }
}
