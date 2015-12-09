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
        if (gameObject.transform.position.y >= levelGoal)
        {
            Application.LoadLevel(nextLevel);
        }

    }
}
