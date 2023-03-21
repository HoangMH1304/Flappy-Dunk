using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level")]
[System.Serializable]
public class Level : ScriptableObject
{
    public int level;
    public string description;
    public GameObject levlePrefab;
    public GameObject isCompleted;
}
