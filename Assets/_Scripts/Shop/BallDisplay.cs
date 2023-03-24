using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BallDisplay : MonoBehaviour
{
    private Ball ball;
    [Header("Ball Item")]
    [SerializeField] private Image ballSprite;
    [SerializeField] private GameObject mark, lockOverlay;
    [Header("Preview Panel")]
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private Image previewImage;
    [SerializeField] private TextMeshProUGUI condition;


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

    private void OpenHintPanel()
    {
        hintPanel.SetActive(true);
        condition.text = ball.condition;
        previewImage.sprite = ball.sprite;
        hintPanel.GetComponent<HintPanel>().DisplayPreviewImg(ball.type);
    }
    public void OnClick()
    {
        if(PlayerPrefManager.Instance.IsUnlockItem(ball.type, ball.id))
        {
            ShopController.Instance.DeactivateMarkIcon(ball.type, PlayerPrefs.GetInt(ball.type.ToString() + "IdSelected"));
            Debug.Log($"{ball.type}, {ball.id}");
            mark.SetActive(true);
            PlayerPrefs.SetInt(ball.type.ToString() + "IdSelected", ball.id);
            this.PostEvent(EventID.OnChangeSkin);
        }
        else
        {
            OpenHintPanel();
        }
    }
}
