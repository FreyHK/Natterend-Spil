﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public SoundClip[] _clips;
    public AudioMixerGroup AudioMixerGroup;

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

            s.source.outputAudioMixerGroup = AudioMixerGroup;
        }
    }

    public void Play(string _clipName)
    {
        //print("Playing sound: " + _clipName);
        SoundClip s = Array.Find(_clips, SoundClip => SoundClip.clipName == _clipName);

        if (s == null) {
            Debug.LogWarning("Could not find sound: " + _clipName);
            return;
        }

        s.source.Play();

        //Put ourselves on top of player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            transform.position = player.transform.position;
    }

    public void Stop(string _clipName)
    {
        SoundClip s = Array.Find(_clips, SoundClip => SoundClip.clipName == _clipName);
        s.source.Stop();
    }

    public float GetClipLength(string _clipName)
    {
        SoundClip s = Array.Find(_clips, SoundClip => SoundClip.clipName == _clipName);
        print("s" + s.clip);
        return s.clip.length;
    }
}
