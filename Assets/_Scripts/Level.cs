using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level")]
[System.Serializable]
public class Level : ScriptableObject
{
    public int levelID;
    public string description;
    public GameObject levlePrefab;
    public bool GetFinishState()
    {
        return PlayerPrefs.GetInt("Level" + levelID) == 1 ? true : false;
    }

    public void SetFinishState(bool state)
    {
        int result = state ? 1 : 0;
        PlayerPrefs.SetInt("Level" + levelID, result);
    }


}
