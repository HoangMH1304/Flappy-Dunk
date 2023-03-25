using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerPref : MonoBehaviour
{
    [SerializeField] private int highScore;
    [SerializeField] private int lastScore;
    [SerializeField] private bool isTestScore;
    [SerializeField] private bool isTestLevel;

    private void Awake() 
    {
        if(isTestScore)
        {
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.SetInt("LastScore", 0);

        }  
        if(isTestLevel)
        {
            PlayerPrefs.SetInt("Level0", 0);
            PlayerPrefs.SetInt("Level1", 0);
            PlayerPrefs.SetInt("Level2", 0);
        }  
    }
}
