using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundAudioClip
{
    public Sound sound;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume = 1f;
    public float pitch = 1f;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
    
}