using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UILobbyHanlder : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject hoopContainer;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject secondChancePanel;
    // [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private CanvasGroup HUD;
    [SerializeField] private GameObject gameOverPanel;
    
    public void PlayTapSound()
    {
        SoundManager.Instance.PlaySound(Sound.click);
    }

    public void Play()
    {
        // lobbyPanel.GetComponent<RectTransform>().Translate(new Vector3(-8, 0, 0));
        player.SetActive(true);
        hoopContainer.SetActive(true);

        Debug.Log("Start game");
        secondChancePanel.SetActive(false);
        secondChancePanel.transform.DOKill();
        secondChancePanel.transform.DOMoveX(-7, 0f);
        lobbyPanel.transform.DOKill();
        lobbyPanel.transform.DOMoveX(-7, 1f).SetUpdate(true);
        gamePlayPanel.SetActive(true);

        HUD.DOFade(0, 0).SetUpdate(true);
        HUD.DOFade(1, 2).SetUpdate(true);

        gameOverPanel.SetActive(false);

        GameController.Instance.Init();

        SoundManager.Instance.PlaySound(Sound.whistle);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
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
