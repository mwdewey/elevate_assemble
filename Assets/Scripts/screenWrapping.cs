using UnityEngine;
using System.Collections;

public class screenWrapping : MonoBehaviour {

    public float leftBound;
    public float rightBound;
    public float bound_offset;

    private Vector3 v;
    
	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (transform.position.x < leftBound)
        {
            v = transform.position;
            v.x = rightBound - bound_offset;
            transform.position = v;
        }

        else if (transform.position.x > rightBound)
        {
            v = transform.position;
            v.x = leftBound + bound_offset;
            transform.position = v;
        }
    }
}
