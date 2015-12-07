using UnityEngine;
using System.Collections;
using WebSocketSharp;

public class socks : MonoBehaviour {

    WebSocket ws;
    float prevPos;

	void Start () {
        ws = new WebSocket("ws://dvm.io:8081/socket.io/?EIO=3&transport=websocket");

        //ws.OnError += (sender, e) => Debug.Log("ERROR: " + e.Message);

        ws.ConnectAsync();

        prevPos = transform.position.x;
	}

    void Update ()
    {
        if (prevPos != transform.position.x)
        {
            ws.Send("42[\"pos\",\"" + (transform.position.x + 6) + "\"]");
        }
        prevPos = transform.position.x;

    }

    void OnDestroy()
    {
        if (ws != null) ws.Close();
    }
}
