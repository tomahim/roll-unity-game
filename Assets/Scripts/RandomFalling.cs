using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomFalling : MonoBehaviour {
  public GameObject fallingObject;
  public GameObject player;

  public float spawnTime = 0.4f; 
  private float timer = 0;
  private int randomNumber;
  
  void Update () {
    timer += Time.deltaTime;
    if(timer > spawnTime){
      SpawnRandom();
      timer = 0;
    }
  }

  public void SpawnRandom() {
    // Creating random Vector3 position
    float x = UnityEngine.Random.Range(9.820001f, 13f);
    float y = player.transform.position.y + 10f;
    float z = UnityEngine.Random.Range(player.transform.position.z + 10f, player.transform.position.z + 60f);
    Vector3 randomPosition = new Vector3(x, y, z);
    // Instantiation of the Object
    float scale = UnityEngine.Random.Range(80f, 90f);
    fallingObject.transform.localScale = new Vector3(scale, scale, scale);
    Instantiate(fallingObject, randomPosition, Quaternion.identity);
  }
}