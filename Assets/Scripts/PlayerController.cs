using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float minPlayerSpeed = 5.2f;
    public float maxPlayerSpeed = 8.2f;

    public float jumpingHeight = 100f;
    public LevelController levelController;
    public AudioSource fallingSound;
    public AudioSource explodingSound;

    Rigidbody m_Rigidbody;
    Collider m_Collider;
    Vector3 m_Movement; 

    bool willBounce = false;
    float bounceHeight = 20f;

    float acceleration = 0f;
    float accelerationStep = 0.015f;

    private void Start() {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
    }

    private bool isGrounded() {
        float distanceToTheGround = m_Collider.bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, distanceToTheGround + 0.1f);
    }

    private void Update() {
        if (LevelTransition.hasGameStarted) {
            float horizontal = Input.GetAxis ("Horizontal");
            float vertical = Input.GetAxis ("Vertical");

            bool isMoving = horizontal != 0 || vertical != 0;
            if (isMoving) {
                if (acceleration < minPlayerSpeed) {
                    acceleration = minPlayerSpeed;
                } else if (acceleration < maxPlayerSpeed) {
                    acceleration += accelerationStep;
                }
            } else if (acceleration > 0) {
                acceleration = 0;
            }

            m_Movement.Set(horizontal, 0, vertical);
            m_Movement.Normalize();
            m_Rigidbody.AddForce(m_Movement * acceleration);

            if (Input.GetKeyDown ("space") && isGrounded()) {
                m_Rigidbody.AddForce(Vector3.up * jumpingHeight);
            }
            
            if (willBounce) {
                m_Rigidbody.AddForce (0, bounceHeight, 0, ForceMode.Impulse);
                willBounce = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Gem")) {
            levelController.gemRetrieved();
        }
        if (other.gameObject.CompareTag("Respawn")) {
            fallingSound.Play();
            StartCoroutine(LevelTransition.loadLevel(0.8f));
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Explosive")) {
            explodingSound.Play();
            StartCoroutine(LevelTransition.loadLevel(1.2f));
            collision.gameObject.GetComponent<Explodable>().explode();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Trampoline")) {
            willBounce = true;
            m_Rigidbody.AddForce(Vector3.up * 200f);
            Animator trampolineAnimator = collision.gameObject.GetComponent<Animator>();
            trampolineAnimator.SetTrigger("BounceTrigger");
        }
     }
}
