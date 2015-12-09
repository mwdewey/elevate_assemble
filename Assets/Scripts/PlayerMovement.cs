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
    bool isGroundedPrev;

    Vector3 velocity;
    CharacterController controller;
    AudioSource audio_source;

    bool canJump;



    /*
        TWEAKING VALUES, ALL PUBLIC
    */

    public float moveSpeed;
    // I'm using a jump height field because it's more sensible to tweak than a jump speed field.
    //public float JUMP_HEIGHT_INITIAL;
    public float jumpHeight;
    
    // This is how long it takes our character to reach the apex of his/her jump.
    public float timeToApex;
    public float gravity;

    // The initial jump speed, v_i. Editing this value is counterintuitive from the 
    // Unity editor, it makes more sense to tweak jumpHeight & timeToApex (Basically floatiness).
    // This will be calculated on init.
    [HideInInspector]
    public float jumpSpeed;
    //We'll be modifying our jumpspeed based on the platform we're on. Let's save the original jump speed for now.
    private float orig_jumpSpeed;
    //Keep us locked in one z plane.
    private float z_plane;
    //Camera stuff
    public float camera_offset_y = 0.5f;
    public float camera_offset_x = 0.0f;
    public float camera_offset_z = 0.5f;
    public float camera_angle = 0.0f;

    //Audio 
    public AudioClip land_jump_sound;

	// Use this for initialization
	void Start () {
        isMovingHorizontally = false;
        isFacingLeft = false;
        prevFacing = false;
        isGroundedPrev = true;
        canJump = true;
        
        jumpSpeed = jumpHeightToJumpSpeed(jumpHeight);
        orig_jumpSpeed = jumpSpeed;
        controller = GetComponent<CharacterController>();

        audio_source = GetComponent<AudioSource>();
        z_plane = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePlayer();
        UpdateCamera();
    }
    void UpdatePlayer() {
        //The z of the player should never change.
        transform.position = new Vector3(transform.position.x, transform.position.y, z_plane);
        // If we bonk our head on the ceiling then set the y velocity to 0.
        if ((controller.collisionFlags & CollisionFlags.Above) != 0) {
            velocity.y = -gravity * Time.deltaTime;
            Debug.Log("Are we grounded " + controller.isGrounded);
        }
        //JUMPING AND GRAVITY
        //Gravity.
        if (!controller.isGrounded) {
            Debug.Log("You're in the aaiiiiiir");
            
            velocity.y -= gravity * Time.deltaTime;

        } else {

            velocity.y = -gravity * Time.deltaTime;
        }


        // when character lands, play landing sound
        if (isGroundedPrev != controller.isGrounded && controller.isGrounded) {
            float vol = Mathf.Abs(velocity.y) * 0.01f;
            audio_source.PlayOneShot(land_jump_sound, vol);
        }
        isGroundedPrev = controller.isGrounded;
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            //Could potentially add Double or Wall jump. We'll see how far we get.
            if (canJump) {
                velocity = groundedJump(velocity);
            }
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
        Debug.Log(velocity.y);
        controller.Move(velocity * Time.deltaTime);
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
    Vector3 groundedJump(Vector3 inVec) {
        inVec.y = jumpSpeed;
        return inVec;
    }
    void OnControllerColliderHit(ControllerColliderHit hit) {
        if((controller.collisionFlags & CollisionFlags.Below) != 0){
            //Go get the platform attributes.
            PlatformAttributes plat = hit.gameObject.GetComponent<PlatformAttributes>();
            
            jumpSpeed = orig_jumpSpeed;
            // If getcomponent does not return null, and some of its fields are not 0, it means we're on a platform with a different behavior.
            if (plat != null) {
                if (plat.jump_height != 0) {
                    jumpSpeed = jumpHeightToJumpSpeed(plat.jump_height);
                }
            }
            //Jeez there's something really goofy about isGrounded.
            canJump = true;

        }


    }
    
    float jumpHeightToJumpSpeed(float inHeight) {
        /*
            Holy 8th grade physics, batman! Using the standard motion equation:

            v_f^2 = v_i^2 +2ad

            d = jumpheight, v_f^2 = 0 (At the apex), a = -g

            solve for v_i

            v_i = sqrt(-2ad)

        */
        float speed = Mathf.Sqrt(-2 * -gravity * inHeight);
        return speed;
    }
}
