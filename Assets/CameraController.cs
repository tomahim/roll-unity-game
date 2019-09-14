using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

   public GameObject player;   
   public bool isShaking = false;
 
   private Vector3 offset;
 
   // A measure of magnitude for the shake. Tweak based on your preference
   public float shakeMagnitude = 0.7f;
 
   // The initial position of the GameObject
   Vector3 initialPosition;

    // Start is called before the first frame update
    void Start() {
	offset = transform.position - player.transform.position; 
    }

    // Update is called once per frame
    void Update() {
        Vector3 newPosition = player.transform. position + offset;
        if (isShaking) {
	newPosition += (Random.insideUnitSphere * shakeMagnitude);
        } 
        transform.position = newPosition;
    }
}