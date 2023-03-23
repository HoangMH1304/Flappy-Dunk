using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ball", menuName = "ScriptableObject/Ball")]
public class Ball : ScriptableObject
{
    public int id;
    public Sprite sprite;
    public string condition;
    public Shop type;
}
