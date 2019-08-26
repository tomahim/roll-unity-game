using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBoxController : MonoBehaviour
{

    private GameObject menuBox;
    // Start is called before the first frame update
    void Start()
    {
        menuBox = transform.Find("MenuBox").gameObject;
        menuBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKey ("escape")) {
            Time.timeScale = 0;
            menuBox.SetActive(true);
            LevelTransition.gameIsPaused = true;
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
