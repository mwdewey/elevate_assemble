using UnityEngine;
using System.Collections;

public class RisingTide : MonoBehaviour {
    public float riseRate;
    float translation = 0;
	// Use this for initialization
	void Start () {
       
	
	}
	
	// Update is called once per frame
	void Update () {
        translation = Time.deltaTime * riseRate;
        transform.Translate(0, translation, 0);
	}

    void OnTriggerEnter(Collider col)
    {

        if(col.gameObject.GetComponent("ResourceBehavior") == true)
        {
            Destroy(col.gameObject);
        }
    }
}
