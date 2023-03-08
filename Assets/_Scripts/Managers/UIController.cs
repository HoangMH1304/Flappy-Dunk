using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI swishText;
    [SerializeField] private GameObject gameoverUI; 
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject secondChancePanel;
    [SerializeField] private GameObject lobbyUI;

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
        highScoreText.text = "BEST: " + highScore.ToString();
        lastScoreText.text = "LAST: " + lastScore.ToString();
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

    public void DisplayGameOverUI()
    {
        gameoverUI.SetActive(true);
        gameplayUI.SetActive(false);
        SoundManager.Instance.PlaySound(SoundManager.Sound.wrong);
        StartCoroutine(WaitForSecondChancePanel());
        //pop up second chance 
    }

    IEnumerator WaitForSecondChancePanel()
    {
        yield return new WaitForSeconds(3f);
        secondChancePanel.SetActive(true);
        secondChancePanel.GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f).SetEase(Ease.InOutCubic).SetUpdate(true);
    }

    public void SwitchToLobbyUI()
    {
        gameoverUI.SetActive(false);
        secondChancePanel.transform.DOMoveX(-5, 0.01f);
        lobbyUI.GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f).SetEase(Ease.InOutCubic).SetUpdate(true);

        // menuPanel.GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f).SetEase(Ease.OutExpo).SetUpdate(true).OnComplete(() =>
        // {
        //     GameManager.Instance.ChangeToEndlessMode();
        // });
        UpdateScoreUI();
        // UI_Menu.Instance.UpdateScore(GameController.instance.Score);
        HoopManager.Instance.FadeAllHoops(0, 0.8f);
        // GameController.Instance.FadeGameObject(0, 0.8f);
    }
}
