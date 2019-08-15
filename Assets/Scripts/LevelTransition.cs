﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelTransition {

    public static int currentLevelNumber = 1;

    public static IEnumerator nextLevelTransition() {
        currentLevelNumber += 1;
        PlayerPrefs.SetInt("currentLevelNumber", currentLevelNumber);
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("LevelTransition");
    }
    
    public static IEnumerator loadLevel(float waitingTime = 2.5f) {
        yield return new WaitForSeconds(waitingTime);
        SceneManager.LoadScene("Level" + currentLevelNumber);
    }
    
    public static IEnumerator loadLevelWithTransition() {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("LevelTransition");
    }

    public static void newGame() {
        PlayerPrefs.SetInt("currentLevelNumber", 1);
        currentLevelNumber = 1;
        SceneManager.LoadScene("LevelTransition");
    }
}