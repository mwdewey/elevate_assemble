using UnityEngine;
using System.Collections;

public class ResourceBehavior : MonoBehaviour {

    public float lifespan;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(lifespan <= 0)
        {
            Destroy(gameObject);
        }
        lifespan--;

        if()
	
	}
}
