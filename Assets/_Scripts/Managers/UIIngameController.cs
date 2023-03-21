using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class UIIngameController : MonoBehaviour
{
    public static UIIngameController Instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI swishText;
    [SerializeField] private GameObject gameoverUI; 
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject lobbyUI;
    [SerializeField] private GameObject challengeUI;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private GameObject hoopContainer;
    [SerializeField] private SecondChance secondChancePanel;
    [SerializeField] private SfxUiToggle sfxUiToggle;

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

    public void UpdateScoreUI()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        int lastScore = PlayerPrefs.GetInt("LastScore");
        highScoreText.text = "<color=black>Best: </color>" + highScore.ToString();
        lastScoreText.text = "Last: " + lastScore.ToString();
    }

    public void UpdateScoreInGame(int score)
    {
        scoreText.text = score.ToString();
        scoreText.transform.DOScale(1.2f,0.1f).OnComplete(()=> { scoreText.transform.DOScale(1.0f, 0.1f).SetUpdate(true); }).SetUpdate(true);
    }

    public void UpdateSwish(int swish)
    {
        swishText.text = "SWISH!\nX" + swish.ToString();
        swishText.DOKill();
        swishText.transform.DOMoveY(1.5f, 0.001f);
        swishText.DOFade(1f, 0.001f);
        if (swish <= 2)
            swishText.DOColor(Color.green, 0.001f);
        else if (swish == 3)
            swishText.DOColor(Color.blue, 0.001f);
            // swishText.DOColor(SpriteHolder.Instance.darkOrange, 0.001f);
        else swishText.DOColor(Color.red, 0.001f);
        // else swishText.DOColor(SpriteHolder.Instance.darkRed, 0.001f);

        // 0.7 1.5
        swishText.transform.DOMoveY(2.0f, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            swishText.DOFade(0, 0.5f).SetEase(Ease.InCirc);
        }
        );
    }

    public void DisplayOnDeathUI()
    {
        gameoverUI.SetActive(true);
        gameplayUI.SetActive(false);
        SoundManager.Instance.PlaySound(Sound.wrong);
        StartCoroutine(WaitForResultUI());
    }

    IEnumerator WaitForResultUI()
    {
        yield return new WaitForSeconds(3f);
        if (!GameManager.Instance.SecondChance) SecondChanceUIPopup();
        else SwitchToMainMenu();
    }


    private void SecondChanceUIPopup()
    {
        secondChancePanel.gameObject.SetActive(true);
        secondChancePanel.GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f).SetEase(Ease.InOutCubic).SetUpdate(true);
    }

    public void SwitchToMainMenu()
    {
        sfxUiToggle.UpdateSFXUI();
        HoopManager.Instance.FadeAllHoops(0, 0.5f);
        gameoverUI.SetActive(false);
        gameplayUI.SetActive(false);
        secondChancePanel?.transform.DOMoveX(-5, 0.01f);  //0.01
        secondChancePanel?.gameObject.SetActive(false);
        player.SetActive(false);
        hoopContainer.SetActive(false);
        //Ease.InOutCubic
        if(GameManager.Instance.IsEndlessMode)
        {
            lobbyUI.GetComponent<RectTransform>().DOLocalMoveY(0, 0.5f).SetEase(Ease.OutExpo).SetUpdate(true);
            UpdateScoreUI();
        }
        else
        {
            challengeUI.GetComponent<RectTransform>().DOLocalMoveY(0, 0.5f).SetEase(Ease.OutExpo).SetUpdate(true);
            LevelSpawner.Instance.ExitLevelMode();
        }
        UIMenuController.Instance.HandleAnimState();
        GameController.Instance.FadeGameObject(0, 0.1f);
        GameManager.Instance.ChangePhase(GameState.OnBegin);
    }

    public void ActiveReviveState()
    {
        GameController.Instance.ActiveReviveState();
        HoopManager.Instance.TurnOnCollider();
        secondChancePanel.gameObject.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        sfxUiToggle.UpdateSFXUI();
        pauseBtn.SetActive(false);
    }

    public void Esc()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pauseBtn.SetActive(true);
        player.GetComponent<Player>().Jump();
    }
}
