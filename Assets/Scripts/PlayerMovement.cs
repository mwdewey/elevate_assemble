using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    /*
        PRIVATE VALUES, ALL CALCULATED FROM PUBLIC VARIABLES IN THE START FUNCTION
    */
    // Is our object facing to the left?
    bool isFacingLeft;
    // keeps track of where we were previously facing.
    bool prevFacing;
    bool isMovingHorizontally;

    Vector3 velocity;
    CharacterController controller;



    /*
        TWEAKING VALUES, ALL PUBLIC
    */

    public float moveSpeed;
    // I'm using a jump height field because it's more sensible to tweak than a jump speed field.
    public float jumpHeight;
    // This is how long it takes our character to reach the apex of his/her jump.
    public float timeToApex;
    public float gravity;

    // The initial jump speed, v_i. Editing this value is counterintuitive from the 
    // Unity editor, it makes more sense to tweak jumpHeight & timeToApex (Basically floatiness).
    // This will be calculated on init.
    private float jumpSpeed;

    //Camera stuff
    public float cameraOffsetY;
    public float cameraAngle;
	// Use this for initialization
	void Start () {
        isMovingHorizontally = false;
        isFacingLeft = false;
        prevFacing = false;
        /*
            Holy 8th grade physics, batman! Using the standard motion equation:
            
            v_f^2 = v_i^2 +2ad

            d = jumpheight, v_f^2 = 0 (At the apex), a = -g

            solve for v_i

            v_i = sqrt(-2ad)
                        
        */
        
        jumpSpeed = Mathf.Sqrt(-2 * -gravity * jumpHeight);

        controller = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update () {
        UpdatePlayer();
        UpdateCamera();
    }
    void UpdatePlayer() {
        //JUMPING AND GRAVITY
        if (Input.GetKeyDown(KeyCode.Space)) {
            //Could potentially add Double or Wall jump. We'll see how far we get.
            if (controller.isGrounded) {
                velocity = groundedJump(velocity);
            }
        }
        //Gravity.
        if (!controller.isGrounded) {
            velocity.y -= gravity * Time.deltaTime;
        }

        //HORIZONTAL MOVEMENT
        if (Input.GetKey(KeyCode.RightArrow)) {
            isFacingLeft = false;
            velocity.x = moveSpeed;
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            isFacingLeft = true;
            velocity.x = -moveSpeed;
        } else {
            velocity.x = 0;
        }
        // If we are not facing the way we rotate our transform.
        if (isFacingLeft != prevFacing) transform.Rotate(0f, 180f, 0f);
        prevFacing = isFacingLeft;
        controller.Move(velocity * Time.deltaTime);
    }
    void UpdateCamera() {
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos.y = transform.position.y + cameraOffsetY;
        Camera.main.transform.position = cameraPos;
    }
    Vector3 groundedJump(Vector3 inVec) {
        inVec.y = jumpSpeed;
        return inVec;
    }

}
