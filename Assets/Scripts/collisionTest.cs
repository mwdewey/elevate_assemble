using UnityEngine;
using System.Collections;

public class collisionTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("ENTER");
    }

    void OnTriggerStay(Collider col)
    {
        Debug.Log("Stay");
    }

    
}
