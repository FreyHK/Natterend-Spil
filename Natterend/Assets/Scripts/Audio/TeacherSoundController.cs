using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherSoundController : MonoBehaviour
{

    public AudioClip[] StepSounds;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    int stepIndex = 0;

    public void PlayStepSound()
    {
        //Play
        source.clip = StepSounds[stepIndex];
        source.Play();
        //Increment
        stepIndex = (stepIndex + 1) % StepSounds.Length;
    }
}
