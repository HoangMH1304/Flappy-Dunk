using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BallDisplay : MonoBehaviour
{
    private Ball item;
    [Header("Ball Item")]
    [SerializeField] private Image ballSprite;
    [SerializeField] private GameObject mark, lockOverlay, newItem;
    [Header("Preview Panel")]
    [SerializeField] private HintPanel hintPanel;

    private void OnEnable() 
    {
        UpdateItemState();
    }

    public void Show(Ball _item)
    {
        item = _item;
        ballSprite.sprite = item.sprite;
    }

    private void UpdateItemState()
    {
        if (PlayerPrefs.GetInt(item.type.ToString() + "IdSelected") == item.id)
        {
            mark.SetActive(true);
            PlayerPrefs.SetInt(item.type.ToString() + item.id, (int)ItemState.used);
        }
        if (!PlayerPrefManager.Instance.IsLockItem(item.type, item.id))
        {
            lockOverlay.SetActive(false);
            if (PlayerPrefs.GetInt(item.type.ToString() + item.id) == (int)ItemState._new)
                newItem.SetActive(true);
        }
        else
        {
            lockOverlay.SetActive(true);
        }
    }

    private void OpenHintPanel()
    {
        hintPanel.ShowUpPanel();
        hintPanel.DisplayPreviewImg(item);
    }
    public void OnClick()
    {
        if (!PlayerPrefManager.Instance.IsLockItem(item.type, item.id))
        {
            ShopController.Instance.DeactivateMarkIcon(item.type, PlayerPrefs.GetInt(item.type.ToString() + "IdSelected"));
            Logger.Log($"{item.type}, {item.id}");
            newItem.SetActive(false);
            mark.SetActive(true);
            PlayerPrefs.SetInt(item.type.ToString() + "IdSelected", item.id);
            PlayerPrefs.SetInt(item.type.ToString() + item.id, (int)ItemState.used);
            this.PostEvent(EventID.OnChangeSkin);
        }
        else
        {
            
            OpenHintPanel();
        }
    }
}
