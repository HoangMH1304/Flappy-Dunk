using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIChallengeManager : MonoBehaviour
{
    public static UIChallengeManager Instance;
    [SerializeField] private Transform levelContainer;
    [SerializeField] private TextMeshProUGUI processText;
    [SerializeField] private Image processBarIngame;
    [SerializeField] private Image processBarLobby;
    private int totalLevels = 0;
    private int completedLevels;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        int totalCompletedLevel = PlayerPrefs.GetInt("CompletedLevels");
        int totalLevels = PlayerPrefs.GetInt("TotalLevels");
        if (totalLevels == 0) totalLevels = 3;
        processText.text = "" + totalCompletedLevel + "/" + totalLevels;
        processBarIngame.fillAmount = totalCompletedLevel / totalLevels;
        processBarLobby.fillAmount = totalCompletedLevel / totalLevels;
    }

    public void UpdateProcess()
    {
        int totalLevels = levelContainer.childCount;
        for(int i = 0; i < totalLevels; i++)
        {
            LevelController _level = levelContainer.GetChild(i).GetComponent<LevelController>();
            _level.ChangeLevelButtonState();
            if(_level.Level.GetFinishState())
            {
                completedLevels++;
            }
        }
        PlayerPrefs.SetInt("CompletedLevels", completedLevels);
        PlayerPrefs.SetInt("TotalLevels", totalLevels);
        processText.text = "" + completedLevels + "/" + totalLevels;
        processBarIngame.fillAmount = completedLevels / totalLevels;
        processBarLobby.fillAmount = completedLevels / totalLevels;
    }
}
