using UnityEngine;
using System.Collections;

public class ResourceBehavior : MonoBehaviour {

    public float fallRate;
    public string resourceType;
    float translation = 0;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        translation = Time.deltaTime * fallRate;
        transform.Translate(0, -translation, 0);

    }
}
