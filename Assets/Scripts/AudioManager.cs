using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour 
{

    public AudioSource[] ambiantSounds;
    private static AudioManager _instance;
    
    private int index = 0;

    public static AudioManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake() 
    {
        if(_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
            StartCoroutine(Play());
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if(this != _instance)
                Destroy(this.gameObject);
        }
    }

    private static void Randomize(AudioSource[] items)
    {
        System.Random rand = new System.Random();

        // For each spot in the array, pick
        // a random item to swap into that spot.
        for (int i = 0; i < items.Length - 1; i++)
        {
            int j = rand.Next(i, items.Length);
            AudioSource temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }
    }

    public IEnumerator Play() {
        Randomize(ambiantSounds);
        AudioSource currentAudio = ambiantSounds[index];
        currentAudio.Play();
        while(true)
        {   
            if(!currentAudio.isPlaying)
            {   
                if((index + 1) == ambiantSounds.Length) {
                    index = 0;   
                } else {
                    index++;
                }
                currentAudio = ambiantSounds[index];
                currentAudio.Play();
            }
            yield return null;
        }
    }
}