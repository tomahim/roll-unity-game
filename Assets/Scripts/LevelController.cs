using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelController : MonoBehaviour
{
    public Text scoreText;

    public bool enableTimer = false;
    public float timeLimit = 60.0f;
    public Text timerText;
    public AudioSource timerEndedSound;

    private int nbCollectedGems = 0;
    private int nbTotalGems;
    private bool timerEndedExecuted = false;
    
    private void Start() {
        nbTotalGems = GameObject.FindGameObjectsWithTag("Gem").Length;
        updateScore();
    }

    private void Update() {
        checkTimer();
    }

    public void gemRetrieved() {
        nbCollectedGems += 1;
        updateScore();
        if (nbCollectedGems == nbTotalGems) {
            StartCoroutine(LevelTransition.nextLevelTransition()); 
        }
    }

    private void updateScore() {
        scoreText.text = Convert.ToString(nbCollectedGems) + " / " + Convert.ToString(nbTotalGems);
    }

    private void checkTimer() {
        if (enableTimer) {
            timeLimit -= Time.deltaTime;
            timerText.text = Convert.ToString(Math.Round(timeLimit, 0));
            if (timeLimit < 10.0f) {
                timerText.color = new Color(220f/255.0f, 20f/255.0f, 60f/255.0f);
            }
            if (timeLimit <= 0.0f) {
                timerEnded();
            }
        } else {
            timerText.text = "";
        }
    }

    private void timerEnded() {
        timerText.text = "TIME'S UP !";
        if (!timerEndedExecuted) {
            if (!timerEndedSound.isPlaying) {
                timerEndedSound.Play();
            }
            timerEndedExecuted = true;
            StartCoroutine(LevelTransition.loadLevel());
        }
    }
}
