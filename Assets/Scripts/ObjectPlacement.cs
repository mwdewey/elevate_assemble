using UnityEngine;
using System.Collections;

public class ObjectPlacement : MonoBehaviour
{

    GameObject heldObject;
    bool isFacingLeft;
    bool prevFacing;
    bool hasCube;
    AudioSource audio_source;

    public GameObject rock;
    public GameObject grass;
    public GameObject wood;
    public UIBehavior UIHandler;
    public AudioClip placement_clip;

    public float placement_X = 0.0f;
    public float placement_Y= 0.0f;

    // Use this for initialization
    void Start()
    {
        isFacingLeft = false;
        prevFacing = false;
        hasCube = false;
        heldObject = null;
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) ||
            Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3)) spawnObject();

        if (heldObject != null)
        {
            if (heldObject.GetComponent<sticky>().isColliding && hasCube) heldObject.GetComponent<MeshRenderer>().material = heldObject.GetComponent<Materials>().invalid;
            else heldObject.GetComponent<MeshRenderer>().material = heldObject.GetComponent<Materials>().normal;
        }


    }

    void spawnObject()
    {
        // spawn cube
        if (!hasCube)
        {
            // rock
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (UIHandler.rockCount > 0)
                {
                    heldObject = (GameObject)Instantiate(rock, new Vector3(0, 0, 0), Quaternion.identity);
                    UIHandler.rockCount--;
                    audio_source.PlayOneShot(placement_clip, 1f);
                }

                else return;
            }

            // grass
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (UIHandler.grassCount > 0 && false)
                {
                    heldObject = (GameObject)Instantiate(grass, new Vector3(0, 0, 0), Quaternion.identity);
                    UIHandler.grassCount--;
                    audio_source.PlayOneShot(placement_clip, 1f);
                }

                else return;
            }

            // wood
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (UIHandler.woodCount > 0)
                {
                    heldObject = (GameObject)Instantiate(wood, new Vector3(0, 0, 0), Quaternion.identity);
                    UIHandler.woodCount--;
                    audio_source.PlayOneShot(placement_clip, 1f);
                }

                else return;
            }

            heldObject.transform.parent = this.transform;
            heldObject.transform.localPosition = new Vector3(placement_X, placement_Y, 0);
            heldObject.GetComponent<Rigidbody>().isKinematic = true;

            hasCube = true;
        }

        // drop cube if not colliding
        else if (!heldObject.GetComponent<sticky>().isColliding)
        {
            heldObject.transform.parent = null;
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.GetComponent<sticky>().checkSticky = true;

            hasCube = false;
        }
    }


}