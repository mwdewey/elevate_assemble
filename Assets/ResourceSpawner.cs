﻿using UnityEngine;
using System.Collections;

public class ResourceSpawner : MonoBehaviour {
    public GameObject cube;
    // Determines where the cubes will spawn
    public float x;
    public float y;
    public float z;

    //Determines the rate at which the cubes will spawn
    public float percentage;
	// Use this for initialization
	void Start () {

        

	}
	
	// Update is called once per frame
	void Update () {
        if (Random.Range(0, 100) <= percentage)
        {
            Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
        }
    }
}
