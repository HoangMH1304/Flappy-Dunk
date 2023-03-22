using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hoop", menuName = "ScriptableObject/Hoop")]
[System.Serializable]
public class Hoop : ScriptableObject
{
    public int ID;
    public Sprite frontHoopSprite, backHoopSprite, starEffectSprite;
    public string condition;
}
