using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelController : MonoBehaviour
{
    public Text scoreText;
    private int nbCollectedGems = 0;
    private int totalGems;
    
    private void Start() {
        totalGems = GameObject.FindGameObjectsWithTag("Gem").Length;
        updateScore();
    }

    public void gemRetrieved() {
        nbCollectedGems += 1;
        updateScore();
    }

    private void updateScore() {
        scoreText.text = Convert.ToString(nbCollectedGems) + " / " + Convert.ToString(totalGems);
    }
}
