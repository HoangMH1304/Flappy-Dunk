using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WingDisplay : MonoBehaviour
{
    private Wing wing;
    [Header("Wing item")]
    [SerializeField] private Image wingSprite;
    [SerializeField] private GameObject mark, lockOverlay;
    [Header("Preview Panel")]
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private Image previewImage;
    [SerializeField] private TextMeshProUGUI condition;

    public void Show(Wing _item)
    {
        wing = _item;
        wingSprite.sprite = wing.sprite;
        //setactive mark icon or lock icon
        Debug.Log(_item.type.ToString() + _item.id);
        if (PlayerPrefs.GetInt(_item.type.ToString() + "IdSelected") == _item.id)
        {
            mark.SetActive(true);
        }
        if (PlayerPrefManager.Instance.IsUnlockItem(wing.type, wing.id))
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
        condition.text = wing.condition;
        previewImage.sprite = wing.sprite;
        hintPanel.GetComponent<HintPanel>().DisplayPreviewImg(wing.type);
    }
    public void OnClick()
    {
        if(PlayerPrefManager.Instance.IsUnlockItem(wing.type, wing.id))
        {
            ShopController.Instance.DeactivateMarkIcon(wing.type, PlayerPrefs.GetInt(wing.type.ToString() + "IdSelected"));
            mark.SetActive(true);
            PlayerPrefs.SetInt(wing.type.ToString() + "IdSelected", wing.id);
            this.PostEvent(EventID.OnChangeSkin);
        }
        else
        {
            OpenHintPanel();
        }
    }
}
