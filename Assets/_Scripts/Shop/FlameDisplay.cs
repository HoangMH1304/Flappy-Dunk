using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameDisplay : MonoBehaviour
{
    private Flame flame;
    [SerializeField] private Image flameSprite;
    [SerializeField] private GameObject mark, lockOverlay;


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

    public void OnClick()
    {
        if(PlayerPrefManager.Instance.IsUnlockItem(flame.type, flame.id))
        {

        }
    }
}
