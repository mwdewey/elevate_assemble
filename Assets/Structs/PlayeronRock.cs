using UnityEngine;
using System.Collections;

public class PlayeronRock : MonoBehaviour {
    public float PlatformJump;
    float normalJump;
    float newJump;

    // Use this for initialization
    void Start()
    {
        newJump = Mathf.Sqrt(-2 * -GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().gravity * PlatformJump);
        normalJump = Mathf.Sqrt( -2 * -GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().gravity * GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().jumpHeight);


    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {

        if (col.GetComponent("PlayerMovement"))
        {
            col.GetComponent<PlayerMovement>().jumpSpeed = newJump;

        }
    }
    void OnTriggerExit(Collider col)
    {
        
        if (col.GetComponent("PlayerMovement"))
        {
            col.GetComponent<PlayerMovement>().jumpSpeed = normalJump;
            }
        }
    }
