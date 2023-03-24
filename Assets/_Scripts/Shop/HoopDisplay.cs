using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoopDisplay : MonoBehaviour
{
    private Hoop hoop;
    [Header("Hoop Item")]
    [SerializeField] private Image frontHoopSprite;
    [SerializeField] private Image backHoopSprite; 
    [SerializeField] private GameObject mark, lockOverlay;
    [Header("Preview Panel")]
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private Image previewFrontHoop, previewBackHoop;
    [SerializeField] private TextMeshProUGUI condition;


    public void Show(Hoop _item)
    {
        hoop = _item;
        frontHoopSprite.sprite = _item.frontHoopSprite;
        backHoopSprite.sprite = _item.backHoopSprite;
        //setactive mark icon or lock icon
        if (PlayerPrefs.GetInt(_item.type.ToString() + "IdSelected") == _item.id)
        {
            mark.SetActive(true);
        }
        if (PlayerPrefManager.Instance.IsUnlockItem(hoop.type, hoop.id))
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
        condition.text = hoop.condition;
        previewFrontHoop.sprite = hoop.frontHoopSprite;
        previewBackHoop.sprite = hoop.backHoopSprite;
        hintPanel.GetComponent<HintPanel>().DisplayPreviewImg(hoop.type);
    }
    public void OnClick()
    {
        if(PlayerPrefManager.Instance.IsUnlockItem(hoop.type, hoop.id))
        {
            ShopController.Instance.DeactivateMarkIcon(hoop.type, PlayerPrefs.GetInt(hoop.type.ToString() + "IdSelected"));
            Debug.Log($"{hoop.type}, {hoop.id}");
            mark.SetActive(true);
            PlayerPrefs.SetInt(hoop.type.ToString() + "IdSelected", hoop.id);
        }
        else
        {
            OpenHintPanel();
        }
    }
}
