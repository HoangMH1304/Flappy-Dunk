using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILobbyHanlder : MonoBehaviour
{
    [SerializeField] private GameObject lobbyCanvas;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject hoopContainer;
    
    public void PlayTapSound()
    {
        if(PlayerPrefs.GetInt("Sound") == 1)  SoundManager.Instance.PlaySound(SoundManager.Sound.click);
    }

    public void Play()
    {
        player.SetActive(true);
        lobbyCanvas.SetActive(false);
        hoopContainer.SetActive(true);
    }
}
