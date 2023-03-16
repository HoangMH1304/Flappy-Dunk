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
    private int swish = 0;
    private int highScore;
    private int lastScore;
    public int Score { get => score; set => score = value; }
    public int Swish { get => swish; set => swish = value; }
    public int HighScore { get => highScore; set => highScore = value; }
    public int LastScore { get => lastScore; set => lastScore = value; }

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
        highScore = PlayerPrefs.GetInt("HighScore");
        lastScore = PlayerPrefs.GetInt("LastScore");
    }

    public void FadeGameObject(float endValue, float time)
    {
        player.FadeCharacter(endValue, time);
        foreach (SpriteRenderer obj in fadeList)
        {
            obj.DOFade(endValue, time).SetEase(Ease.InCubic).SetUpdate(true);
            // obj.DOFade(endValue, time).SetUpdate(true);
        }
    }

    private void Start() {
        UIController.Instance.UpdateScoreUI();
    }

    public void IncreseScore()
    {
        // CameraScript.Instance.Shake();
        if(swish == 0) score++;
        else
        {
            score += (swish + 1);
            UIController.Instance.UpdateSwish(swish + 1);
        }
        lastScore = score;
        if(score > highScore) highScore = score;
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("LastScore", lastScore);
        UIController.Instance.UpdateScoreInGame(score);
        SoundManager.Instance.PlaySound(Sound.pass);
    }

    public void IncreaseSwitch()
    {
        swish++;
        ActivePerfectForm();
        if(swish == 1) SoundManager.Instance.PlaySound(Sound.x2);
        else if(swish == 2) SoundManager.Instance.PlaySound(Sound.x3);
        else if(swish >= 3) SoundManager.Instance.PlaySound(Sound.x4);
        UIController.Instance.UpdateSwish(swish);
        CameraFollow.Instance.Shake();
        Vibrate();
    }

    private void ResetScore()
    {
        score = 0;
        UIController.Instance.UpdateScoreInGame(score);
    }

    public void Init()
    {
        ResetScore();
        player.RestoreInitialState();
        GameManager.Instance.ChangeState(GameState.OnBegin);
        swish = 0;
        HoopManager.Instance.InitialState(); //
        BackgroundMovement.Instance.InitialPosition();
        CameraFollow.Instance.Reset();
        FadeGameObject(1, 0.05f);    

    }

    public void ActiveReviveState()
    {
        GameManager.Instance.ChangeState(GameState.OnRevive);
        swish = 0;
        player.RestoreInitialState();
        player.transform.position = new Vector3(HoopManager.Instance.GetRevivePosition(), 0.315f, 0);
        // ball.GetComponent<Rigidbody2D>().drag = 0;
        CameraFollow.Instance.MoveToBall();
    }

    public void Vibrate()
    {
        if(PlayerPrefs.GetInt("Vibrate") == 1)
        {
            if(swish == 1) MMVibrationManager.Haptic(HapticTypes.LightImpact,true);
            else if(swish > 1) MMVibrationManager.Haptic(HapticTypes.MediumImpact, true);
        }
    }

    public void ActivePerfectForm()
    {
        player.ActivePerfectForm(swish);
    }

    public void DeactivePerfectForm()
    {
        player.DeactivatePerfectForm();
    }
}
