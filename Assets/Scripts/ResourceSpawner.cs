using UnityEngine;
using System.Collections;

public class ResourceSpawner : MonoBehaviour {
    public GameObject ResourceGreen;
    public GameObject ResourceBlue;
    public GameObject ResourceBlack;
    // Determines where the cubes will spawn

    //Determines the rate at which the cubes will spawn
    public float percentageGreen;
    public float percentageBlue;
    public float percentageBlack;
    // Use this for initialization
    void Start () {

        

	}
	
	// Update is called once per frame
	void Update () {
        if (Random.Range(0, 100) <= percentageGreen)
        {
            Instantiate(ResourceGreen, new Vector3(Random.Range(0,10), gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        }
        if (Random.Range(0, 100) <= percentageBlue)
        {
            Instantiate(ResourceBlue, new Vector3(Random.Range(0, 10), gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        }
        if (Random.Range(0, 100) <= percentageBlack)
        {
            Instantiate(ResourceBlack, new Vector3(Random.Range(0, 10), gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        }
    }
}
