using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameDisplay : MonoBehaviour
{
    private Flame flame;
    [SerializeField] private Image flameSprite;

    public void Show(Flame _item)
    {
        flame = _item;
        flameSprite.color = flame.color;
    }
}
