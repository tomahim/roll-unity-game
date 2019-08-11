using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed = 5f;
    public float playerSpeed = 5f;
    public LevelController levelController;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement; 

    void Start() {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        m_Movement.Set(horizontal, 0, vertical);
        m_Movement.Normalize();
        m_Rigidbody.AddForce(m_Movement * playerSpeed);
    }

    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Gem")) {
            levelController.gemRetrieved();
        }
    }
}
