using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelController : MonoBehaviour
{
    public Text scoreText;

    private int nbCollectedGems = 0;
    private int nbTotalGems;
    
    private void Start() {
        nbTotalGems = GameObject.FindGameObjectsWithTag("Gem").Length;
        updateScore();
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
}
