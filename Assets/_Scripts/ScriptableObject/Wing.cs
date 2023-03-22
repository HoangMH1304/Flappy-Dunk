using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wing", menuName = "ScriptableObject/Wing")]
[System.Serializable]
public class Wing : ScriptableObject
{
    public int ID;
    public Sprite sprite;
    public string condition, type;
}
