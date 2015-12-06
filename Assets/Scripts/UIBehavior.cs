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
    float GOtime = 0;
    string level;

    public Text woodDisplay;
    public Text rockDisplay;
    public Text grassDisplay;
    public Text gameOverDisplay;

	// Use this for initialization
	void Start () {
        woodCount = 0;
        rockCount = 0;
        grassCount = 0;
        level =Application.loadedLevelName;

    }
	
	// Update is called once per frame
	void Update () {
        woodDisplay.text = "" + woodCount;
        rockDisplay.text = "" + rockCount;
        grassDisplay.text = grassCount.ToString();
        if (GameObject.FindGameObjectWithTag("Player") == false)
        {
            gameOverDisplay.text = "Game Over";
            GOtime++;
        }
        if(GOtime >= 120)
        {
            Application.LoadLevel(level);
        }
        

    }
}
