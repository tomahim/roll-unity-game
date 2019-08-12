using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed = 5f;
    public float playerSpeed = 5f;
    public float jumpingHeight = 100f;
    public LevelController levelController;
    public AudioSource fallingSound;

    Rigidbody m_Rigidbody;
    Collider m_Collider;
    Vector3 m_Movement; 

    void Start() {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
    }

    bool isGrounded() {
        float distanceToTheGround = m_Collider.bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, distanceToTheGround + 0.1f);
    }

    void Update() {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        m_Movement.Set(horizontal, 0, vertical);
        m_Movement.Normalize();
        m_Rigidbody.AddForce(m_Movement * playerSpeed);

        if (Input.GetKeyDown ("space") && isGrounded()) {
            m_Rigidbody.AddForce(Vector3.up * jumpingHeight);
        }
    }

    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Gem")) {
            levelController.gemRetrieved();
        }
        if (other.gameObject.CompareTag("Respawn")) {
            fallingSound.Play();
            StartCoroutine(LevelTransition.loadLevel(0.7f));
        }
    }
}
