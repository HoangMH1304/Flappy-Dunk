using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flame", menuName = "ScriptableObject/Flame")]
[System.Serializable]
public class Flame : ScriptableObject
{
    public int ID;
    public Color color;
    public string condition, type;
}
