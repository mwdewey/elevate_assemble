using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour {
    public float woodCount = 0;
    public float rockCount = 0;
    public float grassCount = 0;
    float GOtime = 0;
    string level;
    int highestHeight;

    public Sprite grassImage;
    public Sprite stoneImage;
    public Sprite woodImage;

    public Text woodDisplay;
    public Text rockDisplay;
    public Text grassDisplay;
    public Text score;
    public Text gameOverDisplay;
    public Image resource;
    int select = 0;

	// Use this for initialization
	void Start () {
        woodCount = 0;
        rockCount = 0;
        grassCount = 0;
        highestHeight = 0;
        level = Application.loadedLevelName;

    }
	
	// Update is called once per frame
	void Update () {
        if (highestHeight < GameObject.FindGameObjectWithTag("Player").transform.position.y)
        {
            highestHeight = (int)(GameObject.FindGameObjectWithTag("Player").transform.position.y);
        }
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

        //Display Area
        woodDisplay.text = woodCount.ToString();
        rockDisplay.text = rockCount.ToString();
        grassDisplay.text = grassCount.ToString();
        score.text = "Score: " + highestHeight.ToString();

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
