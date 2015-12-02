using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour {
    [HideInInspector]
    public float woodCount = 0;
    [HideInInspector]
    public float rockCount = 0;
    [HideInInspector]
    public float grassCount = 0;

    public Text woodDisplay;
    public Text rockDisplay;
    public Text grassDisplay;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        woodDisplay.text = "" + woodCount;
        rockDisplay.text = "" + rockCount;
        grassDisplay.text = "" + grassCount;

    }
}
