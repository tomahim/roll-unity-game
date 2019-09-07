using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class MuteGameAudio : MonoBehaviour
{  

    public AudioMixer mixer;
    void Start()
    {
        mixer.SetFloat("Music", Mathf.Log10(0.0001f) * 20);
    }
}
