using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject challengePanel;
    [SerializeField] private List<GameObject> level;
    
    public void SelectLevel()
    {
        challengePanel.SetActive(false);
        Instantiate(level[0]);
    }
}
