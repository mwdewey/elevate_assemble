using UnityEngine;
using System.Collections;

public class ResourceSpawner : MonoBehaviour {
    public GameObject ResourceOne;
    public GameObject ResourceTwo;
    public GameObject ResourceThree;
    // Determines where the cubes will spawn

    //Determines the rate at which the cubes will spawn
    public float percentageOne;
    public float percentageTwo;
    public float percentageThree;
    // Use this for initialization
    void Start () {

        

	}
	
	// Update is called once per frame
	void Update () {
        if (Random.Range(0, 100) <= percentageOne)
        {
            Instantiate(ResourceOne, new Vector3(Random.Range(0,10), gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        }
        if (Random.Range(0, 100) <= percentageTwo)
        {
            Instantiate(ResourceTwo, new Vector3(Random.Range(0, 10), gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        }
        if (Random.Range(0, 100) <= percentageThree)
        {
            Instantiate(ResourceThree, new Vector3(Random.Range(0, 10), gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        }
    }
}
