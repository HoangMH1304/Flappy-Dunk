using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIChallengeController : MonoBehaviour
{
    public static UIChallengeController Instance;
    // [SerializeField] private Transform levelContainer;
    [SerializeField] private Image fillBarInChallenge, fillBarInLobby;
    [SerializeField] private TextMeshProUGUI processText;
    // private int totalLevels = 0;
    // private int completedLevels;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UpdateProcess();
    }

    public void UpdateProcess()
    {
        int totalUnlockLevel = ChallengeController.Instance.TotalUnlockLevel;
        int totalLevel = ChallengeController.Instance.TotalLevel;
        processText.text = totalUnlockLevel + "/" + totalLevel;
        Logger.Log($"Process: {totalUnlockLevel}/{totalLevel}");

        float percentOfProcess = 1.0f * totalUnlockLevel / totalLevel;
        fillBarInLobby.fillAmount = percentOfProcess;
        fillBarInLobby.color = ColorContainer.Instance.GetColorByPercent(percentOfProcess);
        fillBarInChallenge.fillAmount = percentOfProcess;
        fillBarInChallenge.color = ColorContainer.Instance.GetColorByPercent(percentOfProcess);
    }
}
