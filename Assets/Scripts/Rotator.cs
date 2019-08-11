using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{   
    public float rotationSpeed = 1f;

    void Update() {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime * rotationSpeed, Space.Self);
    }
}
