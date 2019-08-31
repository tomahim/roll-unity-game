using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class SetVolume : MonoBehaviour
{   

    public AudioMixer mixer;

    void Start () {
        float userPref = PlayerPrefs.GetFloat("musicVolume");
        transform.GetComponent<Slider>().value = userPref > 0f ? userPref : LevelTransition.musicVolume;
        if (userPref > 0f) {
            LevelTransition.musicVolume = userPref;
        }
    }

    public void setLevel(float sliderValue) {
        mixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
        LevelTransition.musicVolume = sliderValue;
        PlayerPrefs.SetFloat("musicVolume", sliderValue);
    }
}
