using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using SimpleJSON;

public class WebSpawner : MonoBehaviour {

    string url = "http://dvm.io/api/getObjects.php";

    public bool run;
    bool prevB;
    Worker w;
    Thread workerThread;

    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public UIBehavior UIHandler;

    void Start()
    {
        run = false;
        prevB = run;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            run = !run;
        }

        // if run just changed
        if (run != prevB)
        {
            // start or end loop
            if (run) startLoop();
            else w.stopTask();
        }

        prevB = run;

        // if any objects exist in queue, spawn
        if (w != null)
        {
            foreach (ResourceObject resObj in w.getResources())
            {
                Debug.Log(resObj.objType + " " + resObj.position);

                Vector3 pos = new Vector3(map((float)resObj.position, 0, 100f, -8f, 8f), 7, -5);

                switch (resObj.objType)
                {
                    case 1:
                        (Instantiate(obj1, pos, Quaternion.identity) as GameObject).GetComponent<ResourceBehavior>().UIhandler = this.UIHandler;
                        break;
                    case 2:
                        (Instantiate(obj2, pos, Quaternion.identity) as GameObject).GetComponent<ResourceBehavior>().UIhandler = this.UIHandler;
                        break;
                    case 3:
                        (Instantiate(obj3, pos, Quaternion.identity) as GameObject).GetComponent<ResourceBehavior>().UIhandler = this.UIHandler;
                        break;
                    default: break;
                }
            }
            w.getResources().Clear();
        }

	}

    void startLoop()
    {
        w = new Worker(url);
        workerThread = new Thread(w.task);
        workerThread.Start();
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    void OnDisable()
    {
        if (w != null) w.stopTask();
    }

    void OnDestroy()
    {
        if (w != null) w.stopTask();
    }
}

class Worker
{
    string resource_url;
    bool run;
    List<ResourceObject> resources;

    public Worker(string _resource_url)
    {
        resource_url = _resource_url;
        run = true;
        resources = new List<ResourceObject>();

        // clear any old resources in DB
        getResourcesWeb();
        resources.Clear();
    }

    public void task()
    {
        while (run)
        {
            getResourcesWeb();
        }

    }

    public void stopTask()
    {
        run = false;
    }

    void getResourcesWeb()
    {
        WebClient client = new WebClient();
        string returnText = client.DownloadString(resource_url);

        var json = JSON.Parse(returnText);

        foreach (JSONNode obj in json.AsArray)
        {
            string objType = obj["objType"];
            string position = obj["pos"];

            int objTypeInt = -1;
            int positionInt = -1;

            if(int.TryParse(objType, out objTypeInt) &&
                int.TryParse(position, out positionInt))
            {
                resources.Add(new ResourceObject(objTypeInt, positionInt));
            }


        }

    }

    public List<ResourceObject> getResources()
    {
        return resources;
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
