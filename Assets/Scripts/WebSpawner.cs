using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using SimpleJSON;
using WebSocketSharp;

public class WebSpawner : MonoBehaviour {

    public float minObjRate;  // objects per second
    float timeSinceLastObj;
    WebSocket ws;
    List<ResourceObject> resources;
    float prevPos;
    int prevScore;

    public GameObject rock;  // index 1
    public GameObject grass; // index 2
    public GameObject wood;  // index 3
    public UIBehavior UIHandler;

    void Start()
    {
        timeSinceLastObj = 0;
        resources = new List<ResourceObject>();
        prevPos = transform.position.x;
        prevScore = Mathf.RoundToInt(transform.position.y);

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
                // from unity, so this would be other unity clients
                case "pos" :
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

            Vector3 pos = new Vector3(map((float)resObj.position, 0, 100f, -6f, 6f),transform.position.y + 7, -5);
            Quaternion rot;
            GameObject objTemp;

            switch (resObj.objType)
            {
                // rock
                case 1:
                    rot = Quaternion.Euler(-90, 0, 0);
                    objTemp = Instantiate(rock, pos, rot) as GameObject;
                    objTemp.GetComponent<ResourceBehavior>().UIhandler = this.UIHandler;
                    break;
                // grass
                case 2:
                    rot = Quaternion.Euler(-90, 0, 0);
                    objTemp = Instantiate(grass, pos, rot) as GameObject;
                    objTemp.GetComponent<ResourceBehavior>().UIhandler = this.UIHandler;
                    break;
                // wood
                case 3:
                    rot = Quaternion.Euler(0, 0, 0);
                    objTemp = Instantiate(wood, pos, rot) as GameObject;
                    objTemp.GetComponent<ResourceBehavior>().UIhandler = this.UIHandler;
                    break;
                default: 
                    break;
            }
        }
        resources.Clear();

        // local object spawner if web is insufficient
        if (timeSinceLastObj > 1f / minObjRate)
        {
            resources.Add(new ResourceObject(Random.Range(1, 3), Random.Range(0, 100)));

            timeSinceLastObj = 0;
        }
        timeSinceLastObj += Time.deltaTime;

        // player tracker
        if (prevPos != transform.position.x)
        {
            ws.SendAsync("42[\"pos\"," + (transform.position.x + 6) + "]",null);
        }
        prevPos = transform.position.x;

        // score tracker
        if (prevScore != Mathf.RoundToInt(transform.position.y))
        {
            ws.SendAsync("42[\"score\"," + Mathf.RoundToInt(transform.position.y) + "]", null);
        }
        prevScore = Mathf.RoundToInt(transform.position.y);

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
