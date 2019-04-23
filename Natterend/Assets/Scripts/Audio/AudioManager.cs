using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public SoundClip[] _clips;

    public static AudioManager Instance;

    void Awake()
    {
        Instance = this;
        foreach (SoundClip s in _clips)
        {           
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.loop = s.loop;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;          
        }
    }

    public void Play(string _clipName)
    {
        SoundClip s = Array.Find(_clips, SoundClip => SoundClip.clipName == _clipName);
        s.source.Play();
    }

    public void Stop(string _clipName)
    {
        SoundClip s = Array.Find(_clips, SoundClip => SoundClip.clipName == _clipName);
        s.source.Stop();
    }
}
