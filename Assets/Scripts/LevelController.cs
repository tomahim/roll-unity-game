using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private int nbCollectedGems = 0;
    
    
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Gem")) {
            nbCollectedGems += 1;
        }
    }
}
