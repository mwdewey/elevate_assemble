using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour {
    public float woodCount = 0;
    public float rockCount = 0;
    public float grassCount = 0;
    float GOtime = 0;
    string level;

    public Sprite grassImage;
    public Sprite stoneImage;
    public Sprite woodImage;

    public Text woodDisplay;
    public Text rockDisplay;
    public Text grassDisplay;
    public Text gameOverDisplay;
    public Image resource;
    int select = 0;

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
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            ResourceSelect();
        }

    }
    void ResourceSelect()
    {
        select++;
        switch (select)
        {
            case 0:
                resource.sprite = grassImage;
                break;
            case 1:
                resource.sprite = stoneImage;
                break;
            case 2:
                resource.sprite = woodImage;
                select = -1;
                break;
        }
            
    }
}
