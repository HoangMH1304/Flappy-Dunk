using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        } 
        highScore = PlayerPrefs.GetInt("HighScore");
        lastScore = PlayerPrefs.GetInt("LastScore");
        // FadeGameObject(0, 0.01f);
    }

    // public void FadeGameObject(float endValue, float time)
    // {
    //     player.Fade(endValue, time);
    //     foreach (SpriteRenderer obj in fadeList)
    //     {
    //         obj.DOFade(endValue, time).SetEase(Ease.InCubic).SetUpdate(true).OnComplete(() =>
    //         {
    //             Time.timeScale = 0;
    //         });
    //     }
    // }

    private void Start() {
        UIController.Instance.UpdateScoreUI();
    }

    public void IncreseScore()
    {
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
        SoundManager.Instance.PlaySound(SoundManager.Sound.pass);
    }

    public void IncreaseSwitch()
    {
        swish++;
        if(swish == 1) SoundManager.Instance.PlaySound(SoundManager.Sound.x2);
        else if(swish == 2) SoundManager.Instance.PlaySound(SoundManager.Sound.x3);
        else if(swish >= 3) SoundManager.Instance.PlaySound(SoundManager.Sound.x4);
        UIController.Instance.UpdateSwish(swish);
    }
}
