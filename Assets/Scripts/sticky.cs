using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sticky : MonoBehaviour {

    public float timeLimit = 1;
    float startTime;
    public bool checkSticky = false;
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
            if (prevPosition.x != transform.position.x ||
                prevPosition.y != transform.position.y ||
                startTime == -1)
            {
                prevPosition = transform.position;
                startTime = Time.realtimeSinceStartup;
            }
                
            else if ((Time.realtimeSinceStartup - startTime) >= timeLimit) Destroy(GetComponent<Rigidbody>());
        }

    }


}
