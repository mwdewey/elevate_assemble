using UnityEngine;
using System.Collections;

public class ObjectPlacement : MonoBehaviour
{

    GameObject cube;
    bool isFacingLeft;
    bool prevFacing;
    bool hasCube;

    public GameObject plank;
    public GameObject corner;
    public GameObject column;

    public float placement_X = 0.0f;
    public float placement_Y= 0.0f;

    // Use this for initialization
    void Start()
    {
        isFacingLeft = false;
        prevFacing = false;
        hasCube = false;
        cube = null;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) ||
            Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3)) spawnObject();

        if (cube != null)
        {
            if (cube.GetComponent<sticky>().isColliding && hasCube) cube.GetComponent<MeshRenderer>().material = cube.GetComponent<Materials>().invalid;
            else cube.GetComponent<MeshRenderer>().material = cube.GetComponent<Materials>().normal;
        }


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
            cube.transform.localPosition = new Vector3(placement_X, placement_Y, 0);
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