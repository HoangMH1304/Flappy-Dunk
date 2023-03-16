using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UILobbyHanlder : MonoBehaviour
{
    public static UILobbyHanlder Instance;
    private const string FIRST_PLAY = "FirstPlay";

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject hoopContainer;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private CanvasGroup tutorialPanel;
    [SerializeField] private GameObject secondChancePanel;
    [SerializeField] private CanvasGroup gameplayPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject playerbtn;
    [SerializeField] private CanvasGroup challengeCanvas;
    [SerializeField] private CanvasGroup storeCanvas;
    [SerializeField] private SfxUiToggle sfxUiToggle;
    private int animState = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Play()
    {
        HandleAnimState();
        hoopContainer.SetActive(true);  //for endless

        secondChancePanel.SetActive(false);
        secondChancePanel.transform.DOKill();
        secondChancePanel.transform.DOMoveX(-7, 0f);

        lobbyPanel.transform.DOKill();  //
        lobbyPanel.transform.DOMoveY(10, 0.5f).SetUpdate(true); //

        gameplayPanel.gameObject.SetActive(true);
        gameplayPanel.DOFade(0, 0).SetUpdate(true);
        gameplayPanel.DOFade(1, 2).SetUpdate(true);

        player.SetActive(true);

        if (PlayerPrefs.GetInt(FIRST_PLAY) == 1)
        {
            PlayerPrefs.SetInt(FIRST_PLAY, 2);
            tutorialPanel.gameObject.SetActive(true);
            tutorialPanel.DOFade(0, 0).SetUpdate(true);
            tutorialPanel.DOFade(1, 1).SetUpdate(true);
        }
        GameController.Instance.Init();

        SoundManager.Instance.PlaySound(Sound.whistle);
    }

    public void Challenge()
    {
        lobbyPanel.transform.DOKill();  //
        lobbyPanel.transform.DOMoveY(10, 0.5f).SetUpdate(true);
        challengeCanvas.gameObject.SetActive(true);
        challengeCanvas.DOFade(0, 0).SetUpdate(true);
        challengeCanvas.DOFade(1, 1f).SetUpdate(true);
    }

    public void Store()
    {
        lobbyPanel.transform.DOKill();  //
        lobbyPanel.transform.DOMoveY(10, 0.5f).SetUpdate(true); //
        storeCanvas.gameObject.SetActive(true);
        storeCanvas.DOFade(0, 0).SetUpdate(true);
        storeCanvas.DOFade(1, 1f).SetUpdate(true);
    }

    public void HandleAnimState()
    {
        animState = 1 - animState;
        playerbtn.GetComponent<Animator>().speed = animState;
    }

    public void ReturnToLobby()
    {
        storeCanvas.gameObject.SetActive(false);
        challengeCanvas.gameObject.SetActive(false);
        lobbyPanel.transform.DOKill();  //
        lobbyPanel.GetComponent<RectTransform>().DOLocalMoveY(0, 0.5f).SetEase(Ease.OutExpo).SetUpdate(true);
    }

    public void ToTestScene()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void TurnOffTutorial()
    {
        tutorialPanel.DOFade(0, 0.5f).SetUpdate(true).OnComplete(() => tutorialPanel.gameObject.SetActive(false));
    }
}
