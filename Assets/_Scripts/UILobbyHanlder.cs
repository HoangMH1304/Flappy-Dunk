using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILobbyHanlder : MonoBehaviour
{
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Sprite viberateOn;
    [SerializeField] private Sprite viberateOff;

    public void PlayTapSound()
    {
        if(PlayerPrefs.GetInt("Sound") == 1)  SoundManager.Instance.PlaySound(SoundManager.Sound.click);
    }
}
