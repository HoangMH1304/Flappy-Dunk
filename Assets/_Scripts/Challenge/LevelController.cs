using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelIndexText;
    [SerializeField] private Image fill;
    private Level level;

    public Level Level { get => level; set => level = value; }

    private void Start()
    {
        levelIndexText.text = level.ID.ToString();
        ChangeLevelButtonState();
    }

    public void CompleteLevel()
    {
        level.SetFinishState(true);
        ChangeLevelButtonState();
    }

    public void ChangeLevelButtonState()
    {
        if(level.GetFinishState())
        {
            fill.color = Color.green;
            levelIndexText.color = Color.white;
        }
        else
        {
            fill.color = Color.white;
            levelIndexText.color = Color.green;
        }
    }

    
}
