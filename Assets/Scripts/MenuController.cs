using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject continueButton;
    public AudioManager audioManager;

    private GameObject confirmMessage;
    private int lastLevelPlayed;

    private void Start() {
        lastLevelPlayed = PlayerPrefs.GetInt("currentLevelNumber");
        confirmMessage = GameObject.Find("ConfirmMessage").GetComponent<Image>().gameObject;
        confirmMessage.SetActive(false);
        LevelTransition.currentLevelNumber = lastLevelPlayed;
        if(lastLevelPlayed == 1) {
            continueButton.SetActive(false);
        }
    }

    public void newGame(bool forceNewGame) {
        if (lastLevelPlayed > 1 && !forceNewGame) {
            showConfirmNewGame(true);
        } else {
            LevelTransition.newGame();
        }
    }

    public void showConfirmNewGame(bool isActive) {
        confirmMessage.gameObject.SetActive(isActive);
    }

    public void continueGame() {
        StartCoroutine(LevelTransition.loadLevel());
    }

    public void quitGame() {
        Application.Quit();
    }
}
