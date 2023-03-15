using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UILobbyHanlder : MonoBehaviour
{
    public static UILobbyHanlder Instance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject hoopContainer;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject secondChancePanel;
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private CanvasGroup HUD;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject playerbtn;
    [SerializeField] private GameObject challengeCanvas;
    [SerializeField] private GameObject storeCanvas;
    [SerializeField] private SfxUiToggle sfxUiToggle;
    private int animState = 1;

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

    public void Play()
    {
        HandleAnimState();
        hoopContainer.SetActive(true);

        secondChancePanel.SetActive(false);
        secondChancePanel.transform.DOKill();
        secondChancePanel.transform.DOMoveX(-7, 0f);

        lobbyPanel.transform.DOKill();
        lobbyPanel.transform.DOMoveX(-7, 0.5f).SetUpdate(true);
        gamePlayPanel.SetActive(true);

        HUD.DOFade(0, 0).SetUpdate(true);
        HUD.DOFade(1, 2).SetUpdate(true);

        player.SetActive(true);
        GameController.Instance.Init();
        gameOverPanel.SetActive(false);


        SoundManager.Instance.PlaySound(Sound.whistle);
    }

    public void Challenge()
    {
        lobbyPanel.SetActive(false);
        challengeCanvas.SetActive(true);
    }

    public void Store()
    {
        lobbyPanel.SetActive(false);
        storeCanvas.SetActive(true);
    }

    public void HandleAnimState()
    {
        animState = 1 - animState;
        playerbtn.GetComponent<Animator>().speed = animState;
    }

    public void ReturnToLobby()
    {
        storeCanvas.SetActive(false);
        challengeCanvas.SetActive(false);
        lobbyPanel.SetActive(true);
    }
}
