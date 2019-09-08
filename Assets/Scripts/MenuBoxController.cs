using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MenuBoxController : MonoBehaviour
{   
    public GameObject cheatChodeField;
    public Text levelCheatNumber;

    private string[] cheatCode;
    private int indexCheatCode;
    private bool cheatCodeActivated = false;
    private GameObject menuBox;

    void Start()
    {
        menuBox = transform.Find("MenuBox").gameObject;
        menuBox.SetActive(false);

        cheatCode = new string[] { "l", "e", "v", "e", "l" };
        indexCheatCode = 0;    
        cheatChodeField.SetActive(false);
    }

    void Update()
    {   
        if (Input.GetKey ("escape")) {
            Time.timeScale = 0;
            menuBox.SetActive(true);
            LevelTransition.gameIsPaused = true;
        }

        if (LevelTransition.gameIsPaused) {
            cheatCodeActivated = indexCheatCode == cheatCode.Length;

            if (!cheatCodeActivated && Input.anyKeyDown) {
                if (Input.GetKeyDown(cheatCode[indexCheatCode])) {
                    indexCheatCode++;
                }
                else {
                    indexCheatCode = 0;    
                }
            }

            if (cheatCodeActivated) {
                if (levelCheatNumber.text != "" && Input.GetKeyDown ("e")) {
                    if (Int32.Parse(levelCheatNumber.text) >= 1 && Int32.Parse(levelCheatNumber.text) <= 22) {
                        continueGame();
                        StartCoroutine(LevelTransition.loadLevelNumber(levelCheatNumber.text));
                    }
                }
            }
            
            if (cheatCodeActivated) {
                // Cheat code successfully inputted!
                cheatChodeField.SetActive(true);
            }
        }
    }

    public void continueGame() {
        menuBox.SetActive(false);
        LevelTransition.gameIsPaused = false;
        Time.timeScale = 1;
    }


    public void retry() {
        menuBox.SetActive(false);
        Time.timeScale = 1;
        LevelTransition.gameIsPaused = false;
        StartCoroutine(LevelTransition.loadLevel(0.5f));
    }

    public void quit() {
        Application.Quit();
    }
}
