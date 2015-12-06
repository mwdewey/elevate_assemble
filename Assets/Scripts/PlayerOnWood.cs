using UnityEngine;
using System.Collections;

public class PlayerOnWood : MonoBehaviour {
    public float OnPlatformSpeed;
    float normalSpeed; 

    // Use this for initialization
    void Start () {
        normalSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().moveSpeed;


    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider col)
    {
       
        if(col.GetComponent("PlayerMovement"))
        {
            col.GetComponent<PlayerMovement>().moveSpeed = OnPlatformSpeed;
           
        }
    }
    void OnTriggerExit(Collider col)
    {
        
        if (col.GetComponent("PlayerMovement"))
        {
            col.GetComponent<PlayerMovement>().moveSpeed = normalSpeed;
        }
    }
}
