using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

public class SfxUiToggle : MonoBehaviour
{
    private const string SOUND = "Sound";
    private const string VIBRATE = "Vibrate";
    private const string FIRST_PLAY = "FirstPlay";
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Sprite vibrateOn;
    [SerializeField] private Sprite vibrateOff;
    [SerializeField] private List<Image> sound;
    [SerializeField] private List<Image> vibrate;
    private Sprite soundImg, vibrateImg;
    private int soundState;
    private int vibrateState;
    
    private void OnEnable() 
    {
        UpdateSFXUI();
    }

    public void UpdateSFXUI()
    {
        if(PlayerPrefs.GetInt(FIRST_PLAY) == 0)
        {
            PlayerPrefs.SetInt(FIRST_PLAY, 1);
            PlayerPrefs.SetInt(SOUND, 1);
            PlayerPrefs.SetInt(VIBRATE, 1);
        }
        soundState = PlayerPrefs.GetInt(SOUND);
        vibrateState = PlayerPrefs.GetInt(VIBRATE);
        Logger.Log($"soundState: {soundState}");
        Logger.Log($"vibrateState: {vibrateState}");
        ChangeIcon();
    }

    public void SwitchSoundState()
    {
        soundState = 1 - soundState;
        PlayerPrefs.SetInt(SOUND, soundState);
        if(soundState == 1) PlayTapSound();
        ChangeIcon();
    }

    public void SwitchVibrationState()
    {
        vibrateState = 1 - vibrateState;
        PlayerPrefs.SetInt(VIBRATE, vibrateState);
        if(vibrateState == 1) MMVibrationManager.Haptic(HapticTypes.LightImpact,true);
        ChangeIcon();
    }

    private void ChangeIcon()
    {
        if(soundState == 1) soundImg = soundOn;
        else soundImg = soundOff;
        if(vibrateState == 1) vibrateImg = vibrateOn;
        else vibrateImg = vibrateOff;

        for(int i = 0; i < sound.Count; i++)
        {
            sound[i].sprite = soundImg;
            vibrate[i].sprite = vibrateImg;
        }
    }

    public void PlayTapSound()
    {
        SoundManager.Instance.PlaySound(Sound.click);
    }
}
