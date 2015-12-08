using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceBehavior : MonoBehaviour {

    public float fallRate;
    public string resourceType;
    public UIBehavior UIhandler;
    float translation = 0;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<PlayerMovement>() == true)
        {
            if(resourceType == "grass")
            {
               UIhandler.grassCount += 1;
            }else if(resourceType == "rock")
            {
                UIhandler.rockCount += 1;
            }else if(resourceType == "wood")
            {
               UIhandler.woodCount += 1;
            }
            Destroy(gameObject);
        }
    }



}
