using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hoop", menuName = "ScriptableObject/Item/Hoop")]
[System.Serializable]
public class Hoop : Item
{
    public Sprite frontHoopSprite, backHoopSprite, starEffectSprite;
}
