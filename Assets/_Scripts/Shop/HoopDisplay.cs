using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoopDisplay : MonoBehaviour
{
    private Hoop hoop;
    [SerializeField] private Image frontHoopSprite, backHoopSprite;
    [SerializeField] private GameObject mark, lockOverlay;


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
}
