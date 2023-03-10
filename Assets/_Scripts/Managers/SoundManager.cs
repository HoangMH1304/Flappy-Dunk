using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound{
        bounce,
        explosion,
        crash,
        flap,
        newBestScore,
        pass,
        whistle,
        wrong,
        x2,
        x3,
        x4,
        click
}
public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private SoundAudioClip[] soundAudioClips;
    protected override void Awake() {
        base.Awake();
        foreach (SoundAudioClip s in soundAudioClips)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.pitch = 1;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(Sound sound)
    {
        SoundAudioClip soundAudioClip = GetAudioClip(sound);
        if(soundAudioClip == null) return;
        if(PlayerPrefs.GetInt("Sound") == 1) soundAudioClip.source.Play();
    }

    public SoundAudioClip GetAudioClip(Sound sound)
    {
        foreach(SoundAudioClip soundAudioClip in soundAudioClips)
        {
            if(soundAudioClip.sound == sound)
                return soundAudioClip;
        }
        Logger.Log("Sound " + sound + " not found");
        return null;
    }
}