using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelTransition {

    public static int currentLevelNumber = 1;

    public static IEnumerator nextLevelTransition() {
        currentLevelNumber += 1;
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("LevelTransition");
    }
    
    public static IEnumerator nextLevel() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level" + currentLevelNumber);
    }
}
