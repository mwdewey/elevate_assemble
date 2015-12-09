using UnityEngine;
using System.Collections;

public class LevelGoalScript : MonoBehaviour {
    public string nextLevel;
    public float levelGoal;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.H)) Application.LoadLevel(nextLevel);
        if (Input.GetKeyDown(KeyCode.G)) Application.LoadLevel(Application.loadedLevelName);

        if (gameObject.transform.position.y >= levelGoal)
        {
            Application.LoadLevel(nextLevel);
        }

    }
}
