using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

    Vector3 velocity;
    GameObject cube;
    bool isFacingLeft;
    bool prevFacing;
    bool hasCube;

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

        if (Input.GetKeyDown(KeyCode.F)) spawnObject();


	}

    void updatePlayer()
    {
        Vector3 position = transform.position;

        position.z = -5f;

        if (Input.GetKey(KeyCode.LeftArrow)) position.x -= speed;
        if (Input.GetKey(KeyCode.RightArrow)) position.x += speed;
        if (Input.GetKeyDown(KeyCode.Space)) velocity.y = jumpSpeed;

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
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            cube.transform.parent = this.transform;

            cube.transform.localPosition = new Vector3(1.5f, 0, 0);

            hasCube = true;
        }

        // drop cube
        else
        {
            cube.transform.parent = null;

            hasCube = false;
        }
    }


}