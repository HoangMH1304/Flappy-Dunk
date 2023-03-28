using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using MoreMountains.NiceVibrations;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private Player player;
    [SerializeField] private List<SpriteRenderer> fadeList;
    private int score = 0;
    private int swishStreak = 0;
    private int totalSwish = 0;
    private int maxSwishStreak = 0;
    private int highScore;
    private int lastScore;
    public int Score { get => score; set => score = value; }
    public int SwishStreak { get => swishStreak; set => swishStreak = value; }
    public int HighScore { get => highScore; set => highScore = value; }
    public int LastScore { get => lastScore; set => lastScore = value; }
    public int TotalSwish { get => totalSwish; set => totalSwish = value; }
    public int MaxSwishStreak { get => maxSwishStreak; set => maxSwishStreak = value; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }   
        else
        {
            Destroy(gameObject);
            return;
        } 
    }

    public void FadeEnviroment(float endValue, float time)
    {
        player.FadeCharacter(endValue, time);
        foreach (SpriteRenderer obj in fadeList)
        {
            obj.DOFade(endValue, time).SetEase(Ease.InCubic).SetUpdate(true);
            // obj.DOFade(endValue, time).SetUpdate(true);
        }
    }

    public void IncreseScore()
    {
        if(swishStreak == 0) score++;
        else
        {
            score += (swishStreak + 1);
            UIIngameController.Instance.UpdateSwish(swishStreak + 1);
        }
        // lastScore = score;
        // if(score > highScore) highScore = score;
        // PlayerPrefs.SetInt("HighScore", highScore);
        // PlayerPrefs.SetInt("LastScore", lastScore);
        UIIngameController.Instance.UpdateScoreInGame(score);
        SoundManager.Instance.PlaySound(Sound.pass);
    }

    public void IncreaseSwitch()
    {
        swishStreak++;
        maxSwishStreak = Math.Max(swishStreak, maxSwishStreak);
        totalSwish++;
        ActivePerfectForm();
        if(swishStreak == 1) SoundManager.Instance.PlaySound(Sound.x2);
        else if(swishStreak == 2) SoundManager.Instance.PlaySound(Sound.x3);
        else if(swishStreak >= 3) SoundManager.Instance.PlaySound(Sound.x4);
        UIIngameController.Instance.UpdateSwish(swishStreak);
        CameraFollow.Instance.Shake();
        Vibrate();
    }

    private void ResetScore()
    {
        score = 0;
        UIIngameController.Instance.UpdateScoreInGame(score);
    }

    public void Init()
    {
        ResetScore();
        GameManager.Instance.ChangePhase(GameState.OnBegin);
        switch (GameManager.Instance.Mode)
        {
            case GameMode.Endless:
                HoopManager.Instance.InitialInEndless();
                break;
            case GameMode.Challenge:
                HoopManager.Instance.InitialInChallenge();
                var goal = FindObjectOfType<Goal>();
                goal.enabled = true;
                break;
            default:
                break;
        }
        player.RestoreInitialState();
        swishStreak = 0;
        maxSwishStreak = 0;
        totalSwish = 0;
        BackgroundMovement.Instance.InitialPosition();
        CameraFollow.Instance.InitialCameraPosition();
        FadeEnviroment(1, 0.5f);    

    }

    public void ActiveReviveState()
    {
        GameManager.Instance.ChangePhase(GameState.OnRevive);
        swishStreak = 0;
        player.RestoreInitialState();
        player.transform.position = new Vector3(HoopManager.Instance.GetRevivePosition(), 0.315f, 0);
        // ball.GetComponent<Rigidbody2D>().drag = 0;
        CameraFollow.Instance.MoveToBall();
    }

    public void Vibrate()
    {
        if(PlayerPrefs.GetInt("Vibrate") == 1)
        {
            if(swishStreak == 1) MMVibrationManager.Haptic(HapticTypes.LightImpact,true);
            else if(swishStreak > 1) MMVibrationManager.Haptic(HapticTypes.MediumImpact, true);
        }
    }

    public void ActivePerfectForm()
    {
        player.ActivePerfectForm(swishStreak);
    }

    public void DeactivePerfectForm()
    {
        player.DeactivatePerfectForm();
    }
}
