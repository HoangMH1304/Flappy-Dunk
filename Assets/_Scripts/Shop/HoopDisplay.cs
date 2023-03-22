using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoopDisplay : MonoBehaviour
{
    private Hoop hoop;
    [SerializeField] private Image frontHoopSprite, backHoopSprite;


    public void Show(Hoop _item)
    {
        hoop = _item;
        frontHoopSprite.sprite = _item.frontHoopSprite;
        backHoopSprite.sprite = _item.backHoopSprite;
    }
}
