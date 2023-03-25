using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private Level level;
    [Header("Level Detail")]
    [SerializeField] private TextMeshProUGUI levelId;
    [SerializeField] private Image fill;
    [Header("Preview Panel")]
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private TextMeshProUGUI tittle, condition, confirm;
    [SerializeField] private TextMeshProUGUI levelIndexText;

    public Level Level { get => level; set => level = value; }

    public void Show(Level _level)
    {
        level = _level;
        levelId.text = (level.ID + 1).ToString();
        DisplayLevelItem();
    }

    public void DisplayLevelItem()
    {
        if (PlayerPrefs.GetInt("Level" + level.ID) == 0)
        {
            levelId.color = Color.green;
            fill.color = Color.white;
        }
        else
        {
            levelId.color = Color.white;
            fill.color = Color.green;
        }
    }

    public void OnClick()
    {
        hintPanel.SetActive(true);
        tittle.text = "CHALLENGE " + (level.ID + 1);
        condition.text = level.description;
        if(PlayerPrefs.GetInt("Level" + level.ID) == 0)
        {
            confirm.text = "START";
        }
        else
        {
            confirm.text = "RETRY";
        }
        PlayerPrefs.SetInt("LevelSelected", level.ID);
    }

    // private void Start()
    // {
    //     levelIndexText.text = level.ID.ToString();
    //     ChangeLevelButtonState();
    // }

    // public void CompleteLevel()
    // {
    //     level.SetFinishState(true);
    //     ChangeLevelButtonState();
    // }

    // public void ChangeLevelButtonState()
    // {
    //     if(level.GetFinishState())
    //     {
    //         fill.color = Color.green;
    //         levelIndexText.color = Color.white;
    //     }
    //     else
    //     {
    //         fill.color = Color.white;
    //         levelIndexText.color = Color.green;
    //     }
    // }

    
}
