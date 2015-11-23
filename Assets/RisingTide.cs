using UnityEngine;
using System.Collections;

public class RisingTide : MonoBehaviour {
    Vector3 Current;
	// Use this for initialization
	void Start () {
        Current = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
        Current.y++;
        transform.position = Current;
	
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.GetComponent("ResourceBehavior") == true)
        {
            Destroy(col.gameObject);
        }
    }
}
