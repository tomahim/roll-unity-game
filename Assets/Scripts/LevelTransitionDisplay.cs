using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransitionDisplay : MonoBehaviour
{
    public Text levelText;

    void Start() {
        levelText.text = "Level " + System.Convert.ToString(
            LevelTransition.currentLevelNumber
        );
        StartCoroutine(LevelTransition.loadLevel());
    }
}
