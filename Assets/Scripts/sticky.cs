using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sticky : MonoBehaviour {

    public float timeLimit = 1.0f;
    public float movement_give = 0.2f;

    float startTime;
    public bool checkSticky = false;
    public bool isColliding = false;
    Vector3 prevPosition;
    bool isFrozen;

	// Use this for initialization
	void Start () {
        startTime = -1;
        prevPosition = transform.position;
        isFrozen = false;
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
                Rigidbody rb = this.GetComponent<Rigidbody>();
                //rb.constraints = RigidbodyConstraints.FreezeAll;
                Destroy(rb);
                this.GetComponent<MeshRenderer>().material = this.GetComponent<Materials>().frozen;
                isFrozen = true;
            }
        }

    }

    void OnTriggerStay(Collider c)
    {
        isColliding = true;
    }

    void OnTriggerExit(Collider c)
    {
        isColliding = false;
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        isColliding = true;

        if (isFrozen)
        {
            // if this object is frozen and it's colliding with another frozen object, delete the other frozen object
            if (collisionInfo.gameObject.GetComponent<PlayerMovement>() == true)
            {
                Debug.Log("DESTROY");
                Destroy(this);
            }


        }
    }

    void OnCollisionExit(Collision c)
    {
        isColliding = false;
    }


}
