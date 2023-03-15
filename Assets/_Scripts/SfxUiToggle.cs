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
