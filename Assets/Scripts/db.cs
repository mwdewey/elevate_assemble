using UnityEngine;
using System.Collections;
using SimpleJSON;

public class db : MonoBehaviour {

    public string url = "http://dvm.io/";

    IEnumerator Start()
    {
        WWW www = new WWW("http://dvm.io/");
        yield return www;

        var N = JSON.Parse(www.text);
        Debug.Log(www.text);

    }


}
