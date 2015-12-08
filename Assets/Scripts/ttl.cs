using UnityEngine;
using System.Collections;

public class ttl : MonoBehaviour {

    public float timeToLive;
    float spawnTime;

	void Start () {
        spawnTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        spawnTime += Time.deltaTime;

        if (spawnTime > timeToLive) Destroy(this.gameObject);
	}
}
