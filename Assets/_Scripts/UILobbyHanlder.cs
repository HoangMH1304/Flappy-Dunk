using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobbyHanlder : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject hoopContainer;
    [SerializeField] private GameObject gameplayCanvas;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private GameObject lobbyPanel;
    
    public void PlayTapSound()
    {
        if(PlayerPrefs.GetInt("Sound") == 1)  SoundManager.Instance.PlaySound(SoundManager.Sound.click);
    }

    public void Play()
    {
        lobbyPanel.GetComponent<RectTransform>().Translate(new Vector3(-8, 0, 0));
        player.SetActive(true);
        hoopContainer.SetActive(true);
        gameplayCanvas.SetActive(true);
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
