using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using SimpleJSON;
using WebSocketSharp;

public class WebSpawner : MonoBehaviour {

    public float minObjRate;  // objects per second
    public float spawnHeight;

    float timeSinceLastObj;
    WebSocket ws;
    List<ResourceObject> resources;
    float prevPos;
    float prevScore;
    float prevGrass;
    float prevRock;
    float prevWood;
    float countSync;
    List<GameObject> spawnedResources;


    public GameObject rock;  // index 1
    public GameObject grass; // index 2
    public GameObject wood;  // index 3
    public UIBehavior UIHandler;

    void Start()
    {
        timeSinceLastObj = 0;
        resources = new List<ResourceObject>();
        prevPos = transform.position.x;
        prevScore = UIHandler.highestHeight;
        prevGrass = UIHandler.grassCount;
        prevWood = UIHandler.woodCount;
        prevRock = UIHandler.rockCount;
        spawnedResources = new List<GameObject>();

        ws = new WebSocket("ws://dvm.io:8081/socket.io/?EIO=3&transport=websocket");
        ws.OnError += (sender, e) => Debug.Log("ERROR: " + e.Message);
        ws.OnMessage += (sender, e) => onMessageReceived(sender,e);
        ws.ConnectAsync();
    }

    void onMessageReceived(object sender, MessageEventArgs e)
    {
        try
        {
            var json = JSON.Parse(e.Data.Substring(2));

            string tag = json.AsArray[0];
            JSONNode objJson = json.AsArray[1];

            switch (tag) {
                // from web client
                case "obj" :
                    int objType = objJson["id"].AsInt;
                    int position = objJson["pos"].AsInt;
                    resources.Add(new ResourceObject(objType, position));
                    break;
                // from unity, so this would be other unity clients.  May handle in future
                case "pos" :
                    break;
                case "score":
                    break;
                case "invW":
                    break;
                case "invR":
                    break;
                case "invG":
                    break;
                default :
                    break;
            }

          
        }

        catch (System.Exception error) {/* Debug.log(error.StackTrace) */ }

    }
	
	void Update () {

        // if any objects exist in queue, spawn
        foreach (ResourceObject resObj in resources)
        {
            timeSinceLastObj = 0;
            Debug.Log(resObj.objType + " " + resObj.position);

            float leftBound = GetComponent<screenWrapping>().leftBound;
            float rightBound = GetComponent<screenWrapping>().rightBound;
            Vector3 pos = new Vector3(map((float)resObj.position, 0, 100f, leftBound, rightBound), transform.position.y + spawnHeight, transform.position.z);
            Quaternion rot;
            GameObject objTemp = null;

            switch (resObj.objType)
            {
                // rock
                case 1:
                    rot = Quaternion.Euler(-90, 0, 0);
                    objTemp = Instantiate(rock, pos, rot) as GameObject;
                    break;
                // grass
                case 2:
                    rot = Quaternion.Euler(-90, 0, 0);
                    objTemp = Instantiate(grass, pos, rot) as GameObject;
                    break;
                // wood
                case 3:
                    rot = Quaternion.Euler(0, 0, 0);
                    objTemp = Instantiate(wood, pos, rot) as GameObject;
                    break;
                default: 
                    break;
            }
            objTemp.GetComponent<ResourceBehavior>().UIhandler = this.UIHandler;
            Physics.IgnoreCollision((objTemp.GetComponent<BoxCollider>()), GetComponent<CharacterController>());
            foreach (GameObject gm in spawnedResources)
            {
                if (gm != null) Physics.IgnoreCollision(gm.GetComponent<BoxCollider>(), objTemp.GetComponent<BoxCollider>());
            }
            spawnedResources.Add(objTemp);


        }
        resources.Clear();

        // local object spawner if web is insufficient
        if (timeSinceLastObj > 1f / minObjRate)
        {
            resources.Add(new ResourceObject(Random.Range(1, 4), Random.Range(0, 100)));

            timeSinceLastObj = 0;
        }
        timeSinceLastObj += Time.deltaTime;

        // player tracker
        float mappedX = map(transform.position.x,GetComponent<screenWrapping>().leftBound,
            GetComponent<screenWrapping>().rightBound,0,100);
        if (prevPos != transform.position.x) ws.SendAsync("42[\"pos\"," + mappedX + "]", null);
        prevPos = transform.position.x;

        // score tracker
        if (prevScore != UIHandler.highestHeight) ws.SendAsync("42[\"score\"," + UIHandler.highestHeight + "]", null);
        prevScore = UIHandler.highestHeight;

        // inventory tracker
        if (prevRock != UIHandler.rockCount)   ws.SendAsync("42[\"invR\"," + UIHandler.rockCount + "]", null);
        if (prevWood != UIHandler.woodCount)   ws.SendAsync("42[\"invW\"," + UIHandler.woodCount + "]", null);
        if (prevGrass != UIHandler.grassCount) ws.SendAsync("42[\"invG\"," + UIHandler.grassCount + "]", null);
        if (countSync > 1)
        {
            ws.SendAsync("42[\"invR\"," + UIHandler.rockCount + "]", null);
            ws.SendAsync("42[\"invW\"," + UIHandler.woodCount + "]", null);
            ws.SendAsync("42[\"invG\"," + UIHandler.grassCount + "]", null);
            countSync = 0;
        }
        countSync += Time.deltaTime;
        prevRock  = UIHandler.rockCount;
        prevWood  = UIHandler.woodCount;
        prevGrass = UIHandler.grassCount;

	}

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    void OnDestroy()
    {
        if (ws != null) ws.CloseAsync();
    }
}

class ResourceObject
{
    public int objType;
    public int position;

    public ResourceObject(int objType_, int position_)
    {
        objType = objType_;
        position = position_;
    }


}
