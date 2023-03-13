using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SfxUiToggle : MonoBehaviour
{
    private const string SOUND = "Sound";
    private const string VIBRATE = "Vibrate";
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
        soundState = PlayerPrefs.GetInt(SOUND);
        vibrateState = PlayerPrefs.GetInt(VIBRATE);
        Debug.Log($"soundState: {soundState}");
        Debug.Log($"vibrateState: {vibrateState}");
        ChangeIcon();
    }
    public void SwitchSoundState()
    {
        soundState = 1 - soundState;
        PlayerPrefs.SetInt(SOUND, soundState);
        ChangeIcon();
    }

    public void SwitchVibrationState()
    {
        vibrateState = 1 - vibrateState;
        PlayerPrefs.SetInt(VIBRATE, vibrateState);
        if(vibrateState == 1) GameController.Instance.Vibrate();
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
