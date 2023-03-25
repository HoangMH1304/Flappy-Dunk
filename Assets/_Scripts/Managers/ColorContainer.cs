using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorContainer : MonoBehaviour
{
    public static ColorContainer Instance;
    public Color orange, blue, purple, green, red, yellow, darkRed, darkOrange;
    private void Awake()
    {
        Instance = this;
    }

    public Color GetColorByPercent(float percent)
    {
        if (percent >= 1) return blue;
        if (percent >= 0.8f) return green;
        if (percent >= 0.6f) return yellow;
        if (percent >= 0.4f) return orange;
        if (percent >= 0.2f) return red;
        if (percent >= 0f) return purple;
        return red;
    }
}
