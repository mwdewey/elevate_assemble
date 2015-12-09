using UnityEngine;
using System.Collections.Generic;

public class ObjectPlacement : MonoBehaviour
{

    GameObject heldObject;
    bool isFacingLeft;
    bool prevFacing;
    bool hasCube;
    bool isPlatform;
    AudioSource audio_source;
    List<Material> orgMaterials;

    public GameObject rock;
    public GameObject grass;
    public GameObject wood;
    public GameObject rockColumn;
    public GameObject grassColumn;
    public GameObject woodColumn;
    public Material invalid_material;
    public UIBehavior UIHandler;
    public AudioClip placement_clip;

    public float placement_X = 0.0f;
    public float placement_Y= 0.0f;

    // key bindings
    List<KeyCode> rockSpawn_key;
    List<KeyCode> woodSpawn_key;
    List<KeyCode> grassSpawn_key;
    List<KeyCode> structSwitch_key;
    List<KeyCode> spawnKeys;

    // Use this for initialization
    void Start()
    {
        isFacingLeft = false;
        prevFacing = false;
        hasCube = false;
        isPlatform = true;
        heldObject = null;
        audio_source = GetComponent<AudioSource>();

        orgMaterials = new List<Material>();

        // key bindings
        rockSpawn_key =    new List<KeyCode>() { KeyCode.Alpha2 };
        woodSpawn_key =    new List<KeyCode>() { KeyCode.Alpha3 };
        grassSpawn_key =   new List<KeyCode>() { KeyCode.Alpha1 };
        structSwitch_key = new List<KeyCode>() { KeyCode.BackQuote };

        spawnKeys = new List<KeyCode>();
        spawnKeys.AddRange(rockSpawn_key);
        spawnKeys.AddRange(woodSpawn_key);
        spawnKeys.AddRange(grassSpawn_key);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetKeysDown(spawnKeys)) spawnObject();

        if (GetKeysDown(structSwitch_key)) switchStructure();
        
        // change material if it is invalid
        if (heldObject != null)
        {
            Material[] tempMats = heldObject.GetComponent<MeshRenderer>().materials;
            for(int i = 0; i < tempMats.Length; i++)
            {
                if (heldObject.GetComponent<sticky>().isColliding && hasCube)
                {
                    tempMats[i] = invalid_material;
                }

                else tempMats[i] = orgMaterials[i];
            }
            heldObject.GetComponent<MeshRenderer>().materials = tempMats;

        }


    }

    bool GetKeysDown(List<KeyCode> keys)
    {
        foreach (KeyCode key in keys) if (Input.GetKeyDown(key)) return true;
        return false;
    }

    void spawnObject()
    {
        // spawn cube
        if (!hasCube)
        {
            // if resources don't exist, do nothing
            if (GetKeysDown(rockSpawn_key) && UIHandler.rockCount == 0) return;
            if (GetKeysDown(grassSpawn_key) && UIHandler.grassCount == 0) return;
            if (GetKeysDown(woodSpawn_key) && UIHandler.woodCount == 0) return;

            if (isPlatform)
            {
                if (GetKeysDown(rockSpawn_key)) heldObject = (GameObject)Instantiate(rock, new Vector3(0, 0, 0), Quaternion.identity);
                if (GetKeysDown(woodSpawn_key)) heldObject = (GameObject)Instantiate(wood, new Vector3(0, 0, 0), Quaternion.identity);
                if (GetKeysDown(grassSpawn_key)) heldObject = (GameObject)Instantiate(grass, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                if (GetKeysDown(rockSpawn_key)) heldObject = (GameObject)Instantiate(rockColumn, new Vector3(0, 0, 0), Quaternion.identity);
                if (GetKeysDown(woodSpawn_key)) heldObject = (GameObject)Instantiate(woodColumn, new Vector3(0, 0, 0), Quaternion.identity);
                if (GetKeysDown(grassSpawn_key)) heldObject = (GameObject)Instantiate(grassColumn, new Vector3(0, 0, 0), Quaternion.identity);
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

            if (heldObject.GetComponent<MeshCollider>() == true) heldObject.GetComponent<MeshCollider>().isTrigger = false;
            if (heldObject.GetComponent<BoxCollider>() == true)  heldObject.GetComponent<BoxCollider>().isTrigger  = false;

            switch (heldObject.GetComponent<sticky>().structType)
            {
                case "grass": UIHandler.grassCount--; break;
                case "rock":  UIHandler.rockCount--;  break;
                case "wood":  UIHandler.woodCount--;  break;
            }

            audio_source.PlayOneShot(placement_clip, 1f);

            hasCube = false;
        }
    }

    void switchStructure()
    {
        isPlatform = !isPlatform;

        UIHandler.switchStructIcons(isPlatform);

        if (hasCube)
        {
            string structType = heldObject.GetComponent<sticky>().structType;
            Destroy(heldObject);

            if (isPlatform)
            {
                switch (structType)
                {
                    case "grass": heldObject = (GameObject)Instantiate(grass, new Vector3(0, 0, 0), Quaternion.identity); break;
                    case "rock": heldObject = (GameObject)Instantiate(rock, new Vector3(0, 0, 0), Quaternion.identity); break;
                    case "wood": heldObject = (GameObject)Instantiate(wood, new Vector3(0, 0, 0), Quaternion.identity); break;
                    default: break;
                }
            }

            else
            {
                switch (structType)
                {
                    case "grass": heldObject = (GameObject)Instantiate(grassColumn, new Vector3(0, 0, 0), Quaternion.identity); break;
                    case "rock": heldObject = (GameObject)Instantiate(rockColumn, new Vector3(0, 0, 0), Quaternion.identity); break;
                    case "wood": heldObject = (GameObject)Instantiate(woodColumn, new Vector3(0, 0, 0), Quaternion.identity); break;
                    default: break;
                }
            }

            heldObject.transform.parent = this.transform;
            heldObject.transform.localPosition = new Vector3(placement_X, placement_Y, 0);

            // save org materials
            orgMaterials.Clear();
            foreach (Material m in heldObject.GetComponent<MeshRenderer>().materials) orgMaterials.Add(m);
        }

    }


}