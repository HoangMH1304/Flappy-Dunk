using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HintPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] previewImages;
    private RectTransform rectTransform;
    private void OnEnable() 
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.DOScale(new Vector3(0, 0, 0), 0);
        rectTransform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }

    public void DisplayPreviewImg(Shop type)
    {
        DeactivePreviewImg();
        switch (type)
        {
            case Shop.Ball:
                previewImages[0].SetActive(true);
                break;
            case Shop.Wing:
                previewImages[1].SetActive(true);
                break;
            case Shop.Hoop:
                previewImages[2].SetActive(true);
                break;
            case Shop.Flame:
                previewImages[3].SetActive(true);
                break;
        }
    }

    public void TurnOffHintPanel()
    {
        rectTransform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => gameObject.SetActive(false));
    }

    private void DeactivePreviewImg()
    {
        foreach (var item in previewImages)
        {
            item.SetActive(false);
        }
    }
}
