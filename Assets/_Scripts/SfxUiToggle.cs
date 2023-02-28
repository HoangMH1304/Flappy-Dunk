using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SfxUiToggle : MonoBehaviour
{
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Sprite viberateOn;
    [SerializeField] private Sprite viberateOff;
    [SerializeField] private Image sound;
    [SerializeField] private Image viberate;
    private int soundState;
    private int viberateState;

    private void Start() 
    {
        soundState = PlayerPrefs.GetInt("Sound");
        viberateState = PlayerPrefs.GetInt("Viberate");
        ChangeIcon();
    }
    public void SwitchSoundState()
    {
        soundState = 1 - soundState;
        PlayerPrefs.SetInt("Sound", soundState);
        ChangeIcon();
    }

    public void SwitchViberateState()
    {
        viberateState = 1 - viberateState;
        PlayerPrefs.SetInt("Viberate", viberateState);
        if(viberateState == 1) Handheld.Vibrate();
        ChangeIcon();
    }

    private void ChangeIcon()
    {
        if(soundState == 1) sound.sprite = soundOn;
        else sound.sprite = soundOff;
        if(viberateState == 1) viberate.sprite = viberateOn;
        else viberate.sprite = viberateOff;
    }
}
