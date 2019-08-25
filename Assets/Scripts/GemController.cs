using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{   
    public AudioSource pickupSound;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            pickupSound.Play();
            gameObject.SetActive(false);
        }
    }
}
