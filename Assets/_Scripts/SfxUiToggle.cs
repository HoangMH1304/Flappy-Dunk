using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SfxUiToggle : MonoBehaviour
{
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Sprite vibrateOn;
    [SerializeField] private Sprite vibrateOff;
    [SerializeField] private Image sound;
    [SerializeField] private Image vibrate;
    private int soundState;
    private int vibrateState;

    private void OnEnable() 
    {
        soundState = PlayerPrefs.GetInt("Sound");
        vibrateState = PlayerPrefs.GetInt("Vibrate");
        Debug.Log($"soundState: {soundState}");
        Debug.Log($"vibrateState: {vibrateState}");
        ChangeIcon();
    }
    public void SwitchSoundState()
    {
        soundState = 1 - soundState;
        PlayerPrefs.SetInt("Sound", soundState);
        ChangeIcon();
    }

    public void SwitchVibrationState()
    {
        vibrateState = 1 - vibrateState;
        PlayerPrefs.SetInt("Vibrate", vibrateState);
        if(vibrateState == 1) Handheld.Vibrate();
        ChangeIcon();
    }

    private void ChangeIcon()
    {
        if(soundState == 1) sound.sprite = soundOn;
        else sound.sprite = soundOff;
        if(vibrateState == 1) vibrate.sprite = vibrateOn;
        else vibrate.sprite = vibrateOff;
    }
}
