using UnityEngine;
using System.Collections;

public class cutScene : MonoBehaviour {

    float ScreenWidth;
    float ScreenHeight;
    GameObject cutscene;


	void Start () {
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
        cutscene = transform.Find("cutscene").gameObject;

        cutscene.GetComponent<RectTransform>().sizeDelta = new Vector2(ScreenWidth, ScreenHeight);
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.F)) Destroy(cutscene);
	}
}
