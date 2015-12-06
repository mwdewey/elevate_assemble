using UnityEngine;
using System.Collections;

public class ObjectPlacement : MonoBehaviour
{

    GameObject cube;
    bool isFacingLeft;
    bool prevFacing;
    bool hasCube;

    public GameObject rock;
    public GameObject grass;
    public GameObject wood;
    public UIBehavior UIHandler;

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
            // rock
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (UIHandler.rockCount > 0)
                {
                    cube = (GameObject)Instantiate(rock, new Vector3(0, 0, 0), Quaternion.identity);
                    UIHandler.rockCount--;
                }

                else return;
            }

            // grass
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (UIHandler.grassCount > 0 && false)
                {
                    cube = (GameObject)Instantiate(grass, new Vector3(0, 0, 0), Quaternion.identity);
                    UIHandler.grassCount--;
                }

                else return;
            }

            // wood
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (UIHandler.woodCount > 0)
                {
                    cube = (GameObject)Instantiate(wood, new Vector3(0, 0, 0), Quaternion.identity);
                    UIHandler.woodCount--;
                }

                else return;
            }

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