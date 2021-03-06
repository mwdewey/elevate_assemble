﻿using UnityEngine;
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
    // controller.isGrounded does not work as I would expect on slopes. I'm going to use collision flags to determine
    // if we're grounded.
    bool canJump;
    float timeSinceLastGrounded;
    //Save it so we don't encounter weirdness with bonking our head on the bottom of a platform.
    float orig_stepOffset;
    Vector3 velocity;
    CharacterController controller;
    AudioSource audio_source;

    Animator animator;


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

    //This number affects how long after being grounded the player is still able to jump. Needed to solve a problem
    //Where you sometimes cannot jump if you're running down a slope
    public float slopeFallGracePeriod;
    //Audio 
    public AudioClip land_jump_sound;

	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
        Debug.Log(animator);
        isMovingHorizontally = false;
        isFacingLeft = false;
        prevFacing = false;
        isGroundedPrev = true;
        canJump = true;
        timeSinceLastGrounded = 0;
        jumpSpeed = jumpHeightToJumpSpeed(jumpHeight);
        orig_jumpSpeed = jumpSpeed;
        controller = GetComponent<CharacterController>();
        orig_stepOffset = controller.stepOffset;
        audio_source = GetComponent<AudioSource>();
        z_plane = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePlayer();
        UpdateCamera();
    }
    void UpdatePlayer() {
        timeSinceLastGrounded += Time.deltaTime;
        
        //There's something very very goofy about controller's isGrounded variable.
        if ((controller.collisionFlags & CollisionFlags.Below) != 0) {
            canJump = true;
            if (controller.isGrounded) {
                animator.SetTrigger("isGrounded");
            }
            timeSinceLastGrounded = 0;
        } else {
            canJump = false;
        }
        //The z of the player should never change.
        transform.position = new Vector3(transform.position.x, transform.position.y, z_plane);
        // If we bonk our head on the ceiling then set the y velocity to 0.
        if ((controller.collisionFlags & CollisionFlags.Above) != 0) {
            controller.stepOffset = 0;
            velocity.y = -gravity * Time.deltaTime;
            velocity.x = 0;
            controller.Move(velocity * Time.deltaTime);
        } else {
            controller.stepOffset = orig_stepOffset;
        }
        //JUMPING AND GRAVITY
        //Gravity.
        if (!canJump) {
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
                animator.SetTrigger("jump");
                
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
        //Animation for horizontal movement.
        if(velocity.x != 0) {
            animator.SetBool("walking", true);
        } else {
            animator.SetBool("walking", false);
        }
        // If we are not facing the way we rotate our transform.
        if (isFacingLeft != prevFacing) transform.Rotate(0f, 180f, 0f);
        prevFacing = isFacingLeft;
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
           // canJump = true;

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
