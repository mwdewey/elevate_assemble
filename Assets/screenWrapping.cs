using UnityEngine;
using System.Collections;

public class screenWrapping : MonoBehaviour {
    //Whether or not the attached trigger is for the left or right side of the screen.
    public bool isLeftBoundary;
    public GameObject oppositeBound;
    private GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update () {
        Vector3 boundPos = transform.position;
        boundPos.z = player.transform.position.z;
        transform.position = boundPos;

    }

    void OnTriggerEnter(Collider hit) {
        Vector3 wrappedPos = hit.gameObject.transform.position;
        hit.gameObject.transform.position = wrappedPos;
    }
}
