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
    {   if (Input.GetKey ("escape")) {
            menuBox.SetActive(true);
            LevelTransition.isPaused = true;
        }
    }

    public void continueGame() {
        menuBox.SetActive(false);
        LevelTransition.isPaused = false;
    }


    public void retry() {
        StartCoroutine(LevelTransition.loadLevel(0.5f));
    }

    public void quit() {
        Application.Quit();
    }
}
