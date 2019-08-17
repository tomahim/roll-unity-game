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
    public bool isPlateform = false;
	
    private int current = 0;

	void Update () {
		if(Vector3.Distance(waypoints[current].transform.position, transform.position) < waypointRadius)
        {
            current++;
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);

    }

    void OnTriggerEnter(Collider collider)
    {
        if (isPlateform && collider.gameObject == player) {
            player.transform.SetParent(transform);
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (isPlateform && collider.gameObject == player) {
            player.transform.parent = null;
        }
    }
}
