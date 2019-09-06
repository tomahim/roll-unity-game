using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelController : MonoBehaviour
{
    public bool enableTimer = false;
    public float timeLimit = 60.0f;
    public Text timerText;
    
    private Animator fadeOutAnimator;
    private Text scoreText;
    private Image imageInstruction;
    private Text textInstruction;
    private Image levelImage;
    private Text levelText;
    private AudioSource timerEndedSound;
    private AudioSource levelCompleteSound;
    private AudioSource levelStartSound;
    
    private int nbCollectedGems = 0;
    private int nbTotalGems;
    private bool timerEndedExecuted = false;
    private float startLevelTime = 1.1f;
    
    private void Start() {
        scoreText = GameObject.Find("GameCanvas/ScoreText").GetComponent<Text>();
        imageInstruction = transform.Find("GameCanvas/InstructionBox").GetComponent<Image>();
        textInstruction = transform.Find("GameCanvas/InstructionBox/InstructionsText").GetComponent<Text>();
        levelImage = transform.Find("GameCanvas/LevelBox").GetComponent<Image>();
        levelText = transform.Find("GameCanvas/LevelBox/LevelText").GetComponent<Text>();
        timerEndedSound = transform.Find("Sounds/TimerExpired").GetComponent<AudioSource>();
        levelCompleteSound = transform.Find("Sounds/LevelCompleteSound").GetComponent<AudioSource>();
        levelStartSound = transform.Find("Sounds/LevelStartSound").GetComponent<AudioSource>();
        fadeOutAnimator = transform.Find("GameCanvas/FadinFadeoutImage").GetComponent<Animator>();

        nbTotalGems = GameObject.FindGameObjectsWithTag("Gem").Length;
        updateScore();
        displayInstruction(false);
        LevelTransition.currentLevelNumber = getCurrentLevelNumberFromSceneName();
        setIsSlopyGround();
        setIsPacmanLevel();
        timerText.text = "";
        StartCoroutine(displayLevelNumber());
        if (LevelTransition.currentLevelNumber == 1) {
            StartCoroutine(showInstruction("Move the ball with arrow keys and collect all the gold gems !"));
        }
        if (LevelTransition.currentLevelNumber == 2) {
            StartCoroutine(showInstruction("Jump with space bar and be carefull not to fall !"));
        }
    }

    private static void setIsSlopyGround() {
        LevelTransition.isSlopyGround = (
            LevelTransition.currentLevelNumber == 5 || 
            LevelTransition.currentLevelNumber == 12 || 
            LevelTransition.currentLevelNumber == 16 || 
            LevelTransition.currentLevelNumber == 18 || 
            LevelTransition.currentLevelNumber == 19
        );
    }

    private static void setIsPacmanLevel() {
        LevelTransition.isPacmanLevel = LevelTransition.currentLevelNumber == 8;
    }

    private void Update() {
        if (LevelTransition.hasGameStarted) {
            checkTimer();
        }
    }

    public void gemRetrieved() {
        nbCollectedGems += 1;
        updateScore();
        if (nbCollectedGems == nbTotalGems) {
            levelCompleted();
        }
    }

    private void levelCompleted() {
        levelCompleteSound.Play();
        StartCoroutine(displayLevelComplete());
        StartCoroutine(LevelTransition.nextLevel()); 
    }

    private int getCurrentLevelNumberFromSceneName() {
        string currentSceneName = SceneManager.GetActiveScene().name; 
        string toBeSearched = "Level";
        string levelNum = currentSceneName.Substring(currentSceneName.IndexOf(toBeSearched) + toBeSearched.Length);
        return Int32.Parse(levelNum);
    }

    private void updateScore() {
        scoreText.text = Convert.ToString(nbCollectedGems) + " / " + Convert.ToString(nbTotalGems);
    }

    private IEnumerator showInstruction(String text, float waitingTime = 3.5f) {
        yield return new WaitForSeconds(startLevelTime);
        displayInstruction(true);
        textInstruction.text = text;
        yield return new WaitForSeconds(waitingTime);
        displayInstruction(false);
    }

    private IEnumerator displayLevelNumber() {
        displayLevelText(true, "LEVEL " + LevelTransition.currentLevelNumber);
        yield return new WaitForSeconds(startLevelTime);
        levelStartSound.Play();
        displayLevelText(false);
        LevelTransition.hasGameStarted = true;
    }

    private IEnumerator displayLevelComplete() {
        displayLevelText(true, "LEVEL COMPLETED !");
        yield return new WaitForSeconds(2.5f);
        displayLevelText(false);
        fadeOutAnimator.SetTrigger("Fadout");
    }

    private void displayLevelText(bool show, String text = "") {
        levelImage.enabled = show;
        levelText.enabled = show;
        levelText.text = show ? text : "";
    }

    private void displayInstruction(bool display) {
        imageInstruction.enabled = display;
        textInstruction.enabled = display;
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
