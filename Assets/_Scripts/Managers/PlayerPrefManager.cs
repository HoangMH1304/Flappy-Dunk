using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FileSave
{
    TotalUnlockLevel,
    TotalSkinUnlocked,
    TotalHoopPassed,
    TotalEndlessPlayed,
    TotalSecondChanceActived
}

public class PlayerPrefManager : MonoBehaviour
{
    public static PlayerPrefManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public bool IsLockItem(Shop type, int id)
    {
        return PlayerPrefs.GetInt(type.ToString() + id) == (int)ItemState.locked ? true : false;
    }

    public int GetUpdatePlayerPref(FileSave a, int step)
    {
        int value = PlayerPrefs.GetInt(a.ToString());
        value += step;
        PlayerPrefs.SetInt(a.ToString(), value);
        return value;
    }

    public int GetPlayerPrefValue(FileSave a)
    {
        return PlayerPrefs.GetInt(a.ToString());
    }

    public void InitShopSpec()
    {
        if (PlayerPrefs.GetInt("FirstInitShop") == 0)
        {
            PlayerPrefs.SetInt("FirstInitShop", 1);
            PlayerPrefs.SetInt("Ball0", 1);
            PlayerPrefs.SetInt("Ball1", 1);
            PlayerPrefs.SetInt("Wing0", 1);
            PlayerPrefs.SetInt("Hoop0", 1);
            PlayerPrefs.SetInt("Flame0", 1);
            PlayerPrefs.SetInt(FileSave.TotalSkinUnlocked.ToString(), 5);
        }
    }
}
