using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sticky : MonoBehaviour {

    public float timeLimit = 1.0f;
    public float movement_give = 0.2f;
    public Material frozen_mat;

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
            if (Mathf.Abs(prevPosition.x-transform.position.x) > movement_give ||
                Mathf.Abs(prevPosition.y - transform.position.y) > movement_give ||
                startTime == -1)
            {
                prevPosition = transform.position;
                startTime = Time.realtimeSinceStartup;
            }

            else if ((Time.realtimeSinceStartup - startTime) >= timeLimit)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
                GetComponent<MeshRenderer>().material = frozen_mat;
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
