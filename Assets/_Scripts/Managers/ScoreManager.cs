using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text lastScoreText;
    [SerializeField] private TMP_Text scoreText;
    public static ScoreManager Instance;
    private int score = 0;
    private int highScore;
    private int lastScore;
    public int Score { get => score; set => score = value; }
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
            Destroy(this.gameObject);
        }

        highScore = PlayerPrefs.GetInt("HighScore");
        lastScore = PlayerPrefs.GetInt("LastScore");
    }

    private void Start() 
    {
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        highScoreText.text = "BEST: " + highScore.ToString();
        lastScoreText.text = "LAST: " + lastScore.ToString();
    }

    public void IncreseScore()
    {
        score += 5;
        scoreText.text = score.ToString();
        lastScore = score;
        if(score > highScore) highScore = score;
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("LastScore", lastScore);
    }
}
