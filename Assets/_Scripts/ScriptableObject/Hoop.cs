using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hoop", menuName = "ScriptableObject/Hoop")]
[System.Serializable]
public class Hoop : ScriptableObject
{
    public int id;
    public Sprite frontHoopSprite, backHoopSprite, starEffectSprite;
    public string condition;
    public Shop type;
}
