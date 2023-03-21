using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerPref : MonoBehaviour
{
    [SerializeField] private int highScore;
    [SerializeField] private int lastScore;
    [SerializeField] private bool isTest;

    private void OnEnable() 
    {
        if(isTest)
        {
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.SetInt("LastScore", 0);

        }    
    }
}
