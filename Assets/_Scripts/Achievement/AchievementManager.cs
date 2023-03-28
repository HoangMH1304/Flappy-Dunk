using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;
    [SerializeField] private GameObject newUnlockSkinPanel;
    [SerializeField] private Transform lobbyCanvas;

    #region TestAchievement
    [SerializeField] public int totalpasshoop;
    public int point;
    public int skin;
    public int totalswish;
    public int swishstreak;
    public int totalplayendless;
    public int totalcompletedlevel;
    public int totalactivesecondchance;
    #endregion

    private void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnPassHoop, (param) => OnPassHoop());
        this.RegisterListener(EventID.OnReachPoint, (param) => OnReachPoint());
        this.RegisterListener(EventID.OnCollectSkin, (param) => OnCollectSkin());
        this.RegisterListener(EventID.OnSwish, (param) => OnSwish());
        this.RegisterListener(EventID.OnTotalPlayEndless, (param) => OnTotalPlayEndless());
        this.RegisterListener(EventID.OnCompleteLevel, (param) => OnCompleteLevel());
        this.RegisterListener(EventID.OnActiveSecondChance, (param) => OnActiveSecondChance());
    }

    private void UnlockSkin(Item _item)
    {
        if(PlayerPrefs.GetInt(_item.type.ToString() + _item.id) != (int)ItemState.locked) return;
        GameObject newPanel = Instantiate(newUnlockSkinPanel, lobbyCanvas);
        newPanel.GetComponent<HintPanel>().DisplayPreviewImg(_item);
        PlayerPrefs.SetInt(_item.type.ToString() + _item.id, (int)ItemState._new);
        this.PostEvent(EventID.OnCollectSkin);
    }

    private void OnActiveSecondChance()
    {
        int numOfSecondChance = PlayerPrefManager.Instance.GetUpdatePlayerPref(FileSave.TotalSecondChanceActived, 1);
        totalactivesecondchance = numOfSecondChance;
        switch (numOfSecondChance)
        {
            case 3:
                UnlockSkin(ShopController.Instance.hoops[1]);
                return;
            case 6:
                UnlockSkin(ShopController.Instance.hoops[2]);
                return;
            default:
                return;
        }
    }

    private void OnCompleteLevel()
    {
        int totalLevelUnlocked = PlayerPrefs.GetInt(FileSave.TotalUnlockLevel.ToString());
        totalcompletedlevel = totalLevelUnlocked;
        switch (totalLevelUnlocked)
        {
            case 1:
                UnlockSkin(ShopController.Instance.hoops[3]);
                return;
            case 2:
                UnlockSkin(ShopController.Instance.hoops[4]);
                return;
            case 3:
                UnlockSkin(ShopController.Instance.hoops[5]);
                return;
            default:
                return;
        }
    }

    private void OnTotalPlayEndless()
    {
        int totalPlayEndless = PlayerPrefManager.Instance.GetUpdatePlayerPref(FileSave.TotalEndlessPlayed, 1);
        totalplayendless = totalPlayEndless;
        switch (totalPlayEndless)
        {
            case 5:
                UnlockSkin(ShopController.Instance.flames[1]);
                return;
            default:
                return;
        }
    }

    private void OnSwish()
    {
        totalswish = GameController.Instance.TotalSwish;
        swishstreak = GameController.Instance.MaxSwishStreak;
        if(GameController.Instance.TotalSwish > 10 && GameManager.Instance.IsEndlessMode)
        {
            UnlockSkin(ShopController.Instance.flames[4]);
        }
        if(GameController.Instance.MaxSwishStreak >= 4)
        {
            UnlockSkin(ShopController.Instance.flames[3]);
        }

    }

    private void OnCollectSkin()
    {
        int totalSkinUnlocked = PlayerPrefManager.Instance.GetUpdatePlayerPref(FileSave.TotalSkinUnlocked, 1);
        skin = totalSkinUnlocked;
        UIShopController.Instance.UpdateProcess();
        switch (totalSkinUnlocked)
        {
            case 10:
                UnlockSkin(ShopController.Instance.balls[7]);
                return;
            default:
                return;
        }
    }

    private void OnReachPoint()
    {
        int score = GameController.Instance.Score;
        point = score;
        if(score >= 30) UnlockSkin(ShopController.Instance.balls[4]);
        else if(score == 29) UnlockSkin(ShopController.Instance.balls[2]);
    }

    private void OnPassHoop()
    {
        int totalHoopPass = PlayerPrefManager.Instance.GetUpdatePlayerPref(FileSave.TotalHoopPassed, 1);
        totalpasshoop = totalHoopPass;
        switch (totalHoopPass)
        {
            case 5:
                UnlockSkin(ShopController.Instance.flames[2]);
                UnlockSkin(ShopController.Instance.wings[1]);
                return;
            case 10:
                UnlockSkin(ShopController.Instance.wings[2]);
                return;
            case 20:
                UnlockSkin(ShopController.Instance.wings[3]);
                UnlockSkin(ShopController.Instance.balls[5]);
                return;
            case 30:
                UnlockSkin(ShopController.Instance.balls[3]);
                return;
            case 40:
                UnlockSkin(ShopController.Instance.balls[6]);
                return;
            case 50:
                UnlockSkin(ShopController.Instance.wings[4]);
                return;
            default:
                return;
        }
    }
}
