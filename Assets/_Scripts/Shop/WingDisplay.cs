using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WingDisplay : MonoBehaviour
{
    private Wing wing;
    [SerializeField] private Image wingSprite;
    [SerializeField] private GameObject mark, lockOverlay;



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
}
