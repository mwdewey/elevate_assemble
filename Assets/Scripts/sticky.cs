﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sticky : MonoBehaviour {

    public float timeLimit = 1;
    float startTime;
    public bool checkSticky = false;
    public bool isColliding = false;
    Vector3 prevPosition;

	// Use this for initialization
	void Start () {
        startTime = -1;
        prevPosition = transform.position;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (checkSticky)
        {
            if (Mathf.Abs(prevPosition.x-transform.position.x) > 0.05 ||
                Mathf.Abs(prevPosition.y - transform.position.y) > 0.05 ||
                startTime == -1)
            {
                prevPosition = transform.position;
                startTime = Time.realtimeSinceStartup;
            }

            else if ((Time.realtimeSinceStartup - startTime) >= timeLimit)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

    }

    void OnCollisionStay(Collision collisionInfo)
    {
        isColliding = true;
    }

    void OnCollisionExit(Collision c)
    {
        isColliding = false;
    }


}
