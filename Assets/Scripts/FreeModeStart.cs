using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FreeModeStart : MonoBehaviour {
    float riseRate = 1;
    float translation = 0;
    float time = 0;
    bool openSeq = false;
    string levelLoad;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (openSeq)
        {
            translation = Time.deltaTime * riseRate;
            transform.Translate(0, translation, 0);
            time++;
            if (time >= 180)
            {
                Application.LoadLevel(levelLoad);
            }
        }

    }
    public void openEasy()
    {
        openSeq = true;
        levelLoad = "FreeModeLevelOne";
    }
    public void openNormal()
    {
        openSeq = true;
        levelLoad = "FreeModeLevelTwo";
    }
    public void openHard()
    {
        openSeq = true;
        levelLoad = "FreeModeLevelThree";
    }
    public void openPuzzleMode()
    {
        openSeq = true;
        levelLoad = "PuzzleStageOne";
    }

}
