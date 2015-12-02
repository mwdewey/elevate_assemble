using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

    Vector3 velocity;
    GameObject cube;
    bool isFacingLeft;
    bool prevFacing;
    bool hasCube;

    public GameObject plank;
    public GameObject corner;
    public GameObject column;

    public float speed = 0.1f;
    public float gravity = 0.8f;
    public float jumpSpeed = 0.9f;

    public float camera_offset_y = 0.5f;
    public float camera_angle = 0.0f;

	// Use this for initialization
	void Start () {
        velocity = new Vector3(0, 0, 0);
        isFacingLeft = false;
        prevFacing = false;
        hasCube = false;
        cube = null;
	}
	
	// Update is called once per frame
	void Update () {
        //updatePlayer();

        //updateCamera();

        if (Input.GetKeyDown(KeyCode.Alpha1) ||
            Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3)) spawnObject();


	}

    void updatePlayer()
    {
        Vector3 position = transform.position;

        position.z = -5f;


        position.y += velocity.y;
        transform.position = position;




        prevFacing = isFacingLeft;
    }

    void updateCamera()
    {


    }

    void spawnObject()
    {
        // spawn cube
        if (!hasCube)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) cube = (GameObject)Instantiate(plank, new Vector3(0, 0, 0), Quaternion.identity);
            if (Input.GetKeyDown(KeyCode.Alpha2)) cube = (GameObject)Instantiate(corner, new Vector3(0, 0, 0), Quaternion.identity);
            if (Input.GetKeyDown(KeyCode.Alpha3)) cube = (GameObject)Instantiate(column, new Vector3(0, 0, 0), Quaternion.identity);

            cube.transform.parent = this.transform;
            cube.transform.localPosition = new Vector3(3.0f, 0, 0);
            cube.GetComponent<Rigidbody>().isKinematic = true;

            cube.GetComponents<BoxCollider>();

            hasCube = true;
        }

        // drop cube
        else
        {
            cube.transform.parent = null;
            cube.GetComponent<Rigidbody>().isKinematic = false;
            cube.GetComponent<sticky>().checkSticky = true;

            hasCube = false;
        }
    }


}