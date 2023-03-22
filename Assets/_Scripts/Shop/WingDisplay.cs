using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WingDisplay : MonoBehaviour
{
    private Wing wing;
    [SerializeField] private Image wingSprite;

    public void Show(Wing _item)
    {
        wing = _item;
        wingSprite.sprite = wing.sprite;
    }
}
