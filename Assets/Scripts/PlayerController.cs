using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float minPlayerSpeed = 4.2f;
    public float maxPlayerSpeed = 7.2f;

    public float jumpingHeight = 100f;
    public LevelController levelController;

    Rigidbody m_Rigidbody;
    Collider m_Collider;
    Vector3 m_Movement; 

    bool willBounce = false;
    float bounceHeight = 29f;

    bool willExplode = false;

    float acceleration = 0f;
    float accelerationStep = 0.005f;

    private bool isCurrentlyGrounded = true;

    private System.Random random;
    private AudioSource fallingSound;
    private AudioSource explodingSound;
    private AudioSource trampolineBounceSound;
    private AudioSource bounceGroundSound;

    private void Start() {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
        random = new System.Random();
        fallingSound = GameObject.Find("Sounds/FallingSound").GetComponent<AudioSource>();
        explodingSound = transform.Find("Sounds/ExplodingSound").GetComponent<AudioSource>();
        trampolineBounceSound = transform.Find("Sounds/TrampolineBounceSound").GetComponent<AudioSource>();
        bounceGroundSound = transform.Find("Sounds/BounceGroundSound").GetComponent<AudioSource>();
    }

    private bool isGrounded() {
        float distanceToTheGround = m_Collider.bounds.extents.y;
        float offset = LevelTransition.isSlopyGround ? 1f : 0.1f;
        bool raycast = Physics.Raycast(transform.position, -Vector3.up, distanceToTheGround + offset);
        return raycast;
    }

    private void Update() {

        if (LevelTransition.hasGameStarted && !LevelTransition.gameIsPaused) {
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
            
            if (!isCurrentlyGrounded && isGrounded()) {
                bounceGroundSound.Play();
                isCurrentlyGrounded = true;
            }

            if (Input.GetKeyDown ("space") && isGrounded()) {
                isCurrentlyGrounded = false;
                m_Rigidbody.AddForce(Vector3.up * jumpingHeight);
            }
            
            if (willExplode) {
                m_Rigidbody.AddForce (random.Next(6), 15f, random.Next(6), ForceMode.Impulse);
                willExplode = false;
            }
            if (willBounce) {
                isCurrentlyGrounded = false;
                m_Rigidbody.AddForce (0, bounceHeight, 0, ForceMode.VelocityChange);
                // m_Rigidbody.AddForce(Vector3.up * bounceHeight);
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
            willExplode = true;
            explodingSound.Play();
            StartCoroutine(LevelTransition.loadLevel(1.2f));
            collision.gameObject.GetComponent<Explodable>().explode();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Trampoline")) {
            willBounce = true;
            trampolineBounceSound.Play();
            Animator trampolineAnimator = collision.gameObject.GetComponent<Animator>();
            if (trampolineAnimator) {
                trampolineAnimator.SetTrigger("BounceTrigger");
            }
        }
     }
}
