using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingWaypoints : MonoBehaviour
{

    public GameObject[] waypoints;
    public GameObject player;
    public float speed = 4f;
    public float waypointRadius = 1;
	
    private int current = 0;

	void Update () {
		/*if(Vector3.Distance(waypoints[current].transform.position, transform.position) < waypointRadius)
        {
            current++;
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
        */
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == player && player.transform.parent != transform) {
            Debug.Log("enter1");
            player.transform.SetParent(transform);
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == player) {
            Debug.Log("exit1");
            player.transform.parent = null;
        }
    }

    void OnCollisionEnter(Collision col){
        if (col.gameObject == player){
            Debug.Log("enter");
            player.transform.SetParent(transform);
        }
    }

    void OnCollisionExit(Collision col){
        if (col.gameObject == player){
            Debug.Log("exit");
            player.transform.SetParent(null);
        }
    }
}
