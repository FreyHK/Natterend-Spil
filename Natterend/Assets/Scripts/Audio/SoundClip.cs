using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundClip
{

    public string clipName;

    public AudioClip clip;

    public bool loop;

    [Range(0f, 1f)]
    public float volume = 1;
    [Range(-3f, 3f)]
    public float pitch = 1;


    [HideInInspector]
    public AudioSource source;
}
