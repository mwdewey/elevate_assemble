using UnityEngine;
using System.Collections;

public class RigidCharacterMovement : MonoBehaviour {

    /*
    PRIVATE VALUES, ALL CALCULATED FROM PUBLIC VARIABLES IN THE START FUNCTION
    */
    // Is our object facing to the left?
    bool isFacingLeft;
    // keeps track of where we were previously facing.
    bool prevFacing;
    bool isMovingHorizontally;
    bool isGroundedPrev;

    /*
        PUBLIC VALUES
    */
    public float move_speed;
    //Camera stuff
    public float camera_offset_y = 0.5f;
    public float camera_offset_x = 0.0f;
    public float camera_offset_z = 0.5f;
    public float camera_angle = 0.0f;



    //Audio 
    public AudioClip land_jump_sound;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCamera();
    }
    void UpdateCamera() {
        Vector3 playerPosition = transform.position;
        Vector3 cameraPosition = Camera.main.transform.position;

        cameraPosition.y = playerPosition.y + camera_offset_y;
        cameraPosition.x = camera_offset_x;
        cameraPosition.z = camera_offset_z;

        Camera.main.transform.position = cameraPosition;
        Camera.main.transform.rotation = Quaternion.Euler(camera_angle, 0, 0);

    }
    void FixedUpdate() {
        Rigidbody rb = GetComponent<Rigidbody>();
        
        //the force vector that will end up being added to the player.
        Vector3 move_vec = new Vector3(0, 0, 0);
        //HORIZONTAL MOVEMENT
        if (Input.GetKey(KeyCode.RightArrow)) {
            isFacingLeft = false;
            transform.position = transform.position + new Vector3(move_speed, 0, 0);

        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            isFacingLeft = true;
            transform.position = transform.position + new Vector3(-move_speed, 0, 0);
        } 
        // If we are not facing the way we rotate our transform.
        if (isFacingLeft != prevFacing) transform.Rotate(0f, 180f, 0f);
        prevFacing = isFacingLeft;

        move_vec += move_speed * transform.forward;
    }
}
