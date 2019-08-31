using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject continueButton;
    public AudioManager audioManager;

    private void Start() {
        int lastLevelPlayed = PlayerPrefs.GetInt("currentLevelNumber");
        LevelTransition.currentLevelNumber = lastLevelPlayed;
        if(lastLevelPlayed == 1) {
            continueButton.SetActive(false);
        }
    }

    public void newGame() {
        LevelTransition.newGame();
    }

    public void continueGame() {
        StartCoroutine(LevelTransition.loadLevel());
    }

    public void quitGame() {
        Application.Quit();
    }
}
