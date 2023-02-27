using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
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

    [SerializeField] private SoundAudioClip[] soundAudioClips;
    // [SerializeField] private AudioSource audioSource;

    public void PlaySound(Sound sound)
    {
        SoundAudioClip soundAudioClip = GetAudioClip(sound);
        if(soundAudioClip == null) return;
        soundAudioClip.source.Play();
        // audioSource.PlayOneShot(GetAudioClip(sound));

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