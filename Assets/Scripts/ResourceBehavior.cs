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
        translation = Time.deltaTime * fallRate;
        transform.Translate(0, -translation, 0);

    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.GetComponent("movement") == true)
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
