using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelTransition {

    public static int currentLevelNumber = 1;
    public static bool hasGameStarted = false;
    public static bool gameIsPaused = false;
    public static bool isSlopyGround = false;
    public static float musicVolume = 1.0f;

    public static IEnumerator nextLevel() {
        currentLevelNumber += 1;
        PlayerPrefs.SetInt("currentLevelNumber", currentLevelNumber);
        yield return new WaitForSeconds(2.5f);
        hasGameStarted = false;
        SceneManager.LoadScene("Level" + currentLevelNumber);
    }
    
    public static IEnumerator loadLevel(float waitingTime = 2.5f) {
        yield return new WaitForSeconds(waitingTime);
        hasGameStarted = false;
        SceneManager.LoadScene("Level" + currentLevelNumber);
    }
    
    public static IEnumerator loadLevelWithTransition() {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("LevelTransition");
    }

    public static void newGame() {
        PlayerPrefs.SetInt("currentLevelNumber", 1);
        currentLevelNumber = 1;
        hasGameStarted = false;
        SceneManager.LoadScene("Level" + currentLevelNumber);
    }
}
