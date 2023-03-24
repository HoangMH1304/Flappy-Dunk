using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour
{
    public static PlayerPrefManager Instance;

    private void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("Ball0", 1);
        PlayerPrefs.SetInt("Ball1", 1);
        PlayerPrefs.SetInt("Wing0", 1);
        PlayerPrefs.SetInt("Wing1", 1);
        PlayerPrefs.SetInt("Hoop0", 1);
        PlayerPrefs.SetInt("Hoop1", 1);
        PlayerPrefs.SetInt("Hoop2", 1);
        PlayerPrefs.SetInt("Hoop3", 1);
        PlayerPrefs.SetInt("Hoop4", 1);
        PlayerPrefs.SetInt("Hoop5", 1);
        PlayerPrefs.SetInt("Flame0", 1);
        PlayerPrefs.SetInt("Flame1", 1);
        PlayerPrefs.SetInt("Flame2", 1);
        PlayerPrefs.SetInt("Flame3", 1);
        PlayerPrefs.SetInt("Flame4", 1);
        PlayerPrefs.SetInt("Flame5", 1);
    }

    public bool IsUnlockItem(Shop type, int id)
    {
        return PlayerPrefs.GetInt(type.ToString() + id) == 1 ? true : false;
    }
}
