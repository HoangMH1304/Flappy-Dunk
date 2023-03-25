using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeController : MonoBehaviour
{
    public static ChallengeController Instance;
    public Level[] levels;
    [SerializeField] private Transform levelHolder;
    private int totalLevel, totalUnlockLevel;

    public int TotalLevel { get => totalLevel; set => totalLevel = value; }
    public int TotalUnlockLevel { get => totalUnlockLevel; set => totalUnlockLevel = value; }
    private List<LevelController> levelControllers = new List<LevelController>();

    private void Awake() 
    {
        Instance = this;
        totalLevel = levels.Length;
        totalUnlockLevel = PlayerPrefs.GetInt("TotalUnlockLevel");   
    }

    private void Start() 
    {
        // totalLevel = levels.Length;
        // totalUnlockLevel = PlayerPrefs.GetInt("TotalUnlockLevel");   
        InitLevel();
    }

    public void IncreseLevelCompleted(int levelID)
    {
        int levelState = PlayerPrefs.GetInt("Level" + levelID);
        if(levelState == 1) return;
        PlayerPrefs.SetInt("Level" + levelID, 1);
        totalUnlockLevel++;
        PlayerPrefs.SetInt("TotalUnlockLevel", totalUnlockLevel);
        UIChallengeController.Instance.UpdateProcess();
        UpdateLevelState();
    }

    private void InitLevel()
    {
        GameObject levelTemplate = levelHolder.GetChild(0).gameObject;
        foreach(var level in levels)
        {
            GameObject newLevel = Instantiate(levelTemplate, levelHolder);
            LevelController levelController = newLevel.GetComponent<LevelController>();
            levelController.Show(level);
            levelControllers.Add(levelController);
        }
        Destroy(levelTemplate);
    }

    public void UpdateLevelState()
    {
        foreach (var levelController in levelControllers)
        {
            levelController.DisplayLevelItem();
        }
    }

}
