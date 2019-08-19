using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour {
    public GameObject explosionEffect;

    public void explode() {
        Instantiate(explosionEffect, transform.position, transform.rotation);
    }
}
