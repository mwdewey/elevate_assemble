using UnityEngine;
using System.Collections.Generic;

public class ObjectPlacement : MonoBehaviour
{

    GameObject heldObject;
    bool isFacingLeft;
    bool prevFacing;
    bool hasCube;
    AudioSource audio_source;
    List<Material> orgMaterials;

    public GameObject rock;
    public GameObject grass;
    public GameObject wood;
    public Material invalid_material;
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

        orgMaterials = new List<Material>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) ||
            Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3)) spawnObject();

        if (heldObject != null)
        {
            Material[] tempMats = heldObject.GetComponent<MeshRenderer>().materials;
            for(int i = 0; i < tempMats.Length; i++)
            {
                if (heldObject.GetComponent<sticky>().isColliding && hasCube)
                {
                    Debug.Log("BLERFDSDFSFDG");
                    tempMats[i] = invalid_material;
                }

                else tempMats[i] = orgMaterials[i];
            }
            heldObject.GetComponent<MeshRenderer>().materials = tempMats;

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
                if (UIHandler.grassCount > 0)
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

            // save org materials
            orgMaterials.Clear();
            foreach (Material m in heldObject.GetComponent<MeshRenderer>().materials) orgMaterials.Add(m);

            hasCube = true;
        }

        // drop cube if not colliding
        else if (!heldObject.GetComponent<sticky>().isColliding)
        {
            heldObject.transform.parent = null;
            heldObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            heldObject.GetComponent<sticky>().checkSticky = true;
            heldObject.GetComponent<MeshCollider>().isTrigger = false;

            hasCube = false;
        }
    }


}