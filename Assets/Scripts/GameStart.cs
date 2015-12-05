using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {
    float riseRate = 1;
    float translation = 0;
    float time = 0;
    bool openSeq = false;

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
                Application.LoadLevel("SpawnerTestScene");
            }
        }

    }
   public void Opening()
    {
        openSeq = true;
    }
 
}
