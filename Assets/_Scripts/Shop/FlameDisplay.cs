using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlameDisplay : MonoBehaviour
{
    private Flame flame;
    [Header("Flame Item")]
    [SerializeField] private Image flameSprite;
    [SerializeField] private GameObject mark, lockOverlay;
    [Header("Preview Panel")]
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private Image previewImage;
    [SerializeField] private TextMeshProUGUI condition;


    public void Show(Flame _item)
    {
        flame = _item;
        flameSprite.color = flame.color;
        //setactive mark icon or lock icon
        if (PlayerPrefs.GetInt(_item.type.ToString() + "IdSelected") == _item.id)
        {
            mark.SetActive(true);
        }
        if (PlayerPrefManager.Instance.IsUnlockItem(flame.type, flame.id))
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
        condition.text = flame.condition;
        previewImage.color = flame.color;
        hintPanel.GetComponent<HintPanel>().DisplayPreviewImg(flame.type);
    }
    public void OnClick()
    {
        if(PlayerPrefManager.Instance.IsUnlockItem(flame.type, flame.id))
        {
            ShopController.Instance.DeactivateMarkIcon(flame.type, PlayerPrefs.GetInt(flame.type.ToString() + "IdSelected"));
            //Debug.Log($"{flame.type}, {flame.id}");
            mark.SetActive(true);
            PlayerPrefs.SetInt(flame.type.ToString() + "IdSelected", flame.id);
            this.PostEvent(EventID.OnChangeSkin);

        }
        else
        {
            OpenHintPanel();
        }
    }
}
