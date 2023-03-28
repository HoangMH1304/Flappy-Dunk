using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class HintPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] previewImages;
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private TextMeshProUGUI condition;
    private RectTransform rectTransform;
    private Item item;

    public void ShowUpPanel()
    {
        gameObject.SetActive(true);
        rectTransform = GetComponent<RectTransform>();
        rectTransform.DOScale(new Vector3(0, 0, 0), 0);
        rectTransform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }

    public void DisplayPreviewImg(Item _item)
    {
        DeactivePreviewImg();
        item = _item;
        condition.text = item.condition;
        switch (_item.type)
        {
            case Shop.Ball:
                previewImages[0].SetActive(true);
                previewImages[0].GetComponent<Image>().sprite = ((Ball)item).sprite;
                break;
            case Shop.Wing:
                previewImages[1].SetActive(true);
                previewImages[1].GetComponent<Image>().sprite = ((Wing)item).sprite;
                break;
            case Shop.Hoop:
                previewImages[2].SetActive(true);
                previewImages[2].transform.GetChild(0).GetComponent<Image>().sprite = ((Hoop)item).frontHoopSprite;
                previewImages[2].transform.GetChild(1).GetComponent<Image>().sprite = ((Hoop)item).backHoopSprite;
                break;
            case Shop.Flame:
                previewImages[3].SetActive(true);
                previewImages[3].GetComponent<Image>().color = ((Flame)item).color;
                break;
        }
    }

    public void TurnOffHintPanel()
    {
        rectTransform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => gameObject.SetActive(false));
    }

    public void TryUnlockSkin()
    {
        int lastId = PlayerPrefs.GetInt(item.type.ToString() + "IdSelected");
        Logger.LogWarning(item.type.ToString() + "IdSelected: " + lastId);
        PlayerPrefs.SetString("ItemIdLastSelected", item.type.ToString() + " " + lastId);
        PlayerPrefs.SetInt(item.type.ToString() + "IdSelected", item.id);
        gameObject.SetActive(false);
        shopCanvas.SetActive(false);
        GameManager.Instance.ChangePhase(GameState.OnTrial);
    }

    private void DeactivePreviewImg()
    {
        foreach (var item in previewImages)
        {
            item.SetActive(false);
        }
    }

    public void ClosePanel()
    {
        SoundManager.Instance.PlaySound(Sound.click);
        GetComponent<RectTransform>().DOScale(0, 0.2f).SetEase(Ease.InOutSine).OnComplete(() => {
            Destroy(gameObject);
        });
    }
}
