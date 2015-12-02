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
    public Material normal_mat;
    public Material invalid_mat;

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
        updatePlayer();

        updateCamera();

        if (Input.GetKeyDown(KeyCode.Alpha1) ||
            Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3)) spawnObject();

        if (cube.GetComponent<sticky>().isColliding && hasCube) cube.GetComponent<MeshRenderer>().material = invalid_mat;
        else cube.GetComponent<MeshRenderer>().material = normal_mat;
	}

    void updatePlayer()
    {
        Vector3 position = transform.position;

        position.z = -5f;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) position.x -= speed;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) position.x += speed;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.W)) velocity.y = jumpSpeed;

        position.y += velocity.y;
        transform.position = position;

        velocity.y *= gravity;

        // update orientation
        if (Input.GetKey(KeyCode.LeftArrow)) isFacingLeft = true;
        else if (Input.GetKey(KeyCode.RightArrow)) isFacingLeft = false;

        if (isFacingLeft != prevFacing) transform.Rotate(0f, 180f, 0f);

        prevFacing = isFacingLeft;
    }

    void updateCamera()
    {
        Vector3 playerPosition = transform.position;
        Vector3 cameraPosition = Camera.main.transform.position;

        cameraPosition.y = playerPosition.y + camera_offset_y;

        Camera.main.transform.position = cameraPosition;

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

            hasCube = true;
        }

        // drop cube
        else if (!cube.GetComponent<sticky>().isColliding)
        {
            cube.transform.parent = null;
            cube.GetComponent<Rigidbody>().isKinematic = false;
            cube.GetComponent<sticky>().checkSticky = true;

            hasCube = false;
        }
    }

   


}