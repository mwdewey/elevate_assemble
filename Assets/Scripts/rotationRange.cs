using UnityEngine;
using System.Collections;

public class rotationRange : MonoBehaviour {

    public float min;
    public float max;

	void Start () {
	
	}
	
	void FixedUpdate () {
        Vector3 q = transform.rotation.eulerAngles;

        if (q.z < min) q.z = min;
        if (q.z > max) q.z = max;

        transform.rotation = Quaternion.Euler(q);
	}
}
