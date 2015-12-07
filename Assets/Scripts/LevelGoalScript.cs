using UnityEngine;
using System.Collections;

public class LevelGoalScript : MonoBehaviour {
    public int index;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent("PlayerMovement") == true)
        {
            Application.LoadLevel(index);
        }
    }
}
