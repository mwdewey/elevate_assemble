using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour {
    public float woodCount = 0;
    public float rockCount = 0;
    public float grassCount = 0;
    float GOtime = 0;
    string level;
    public int highestHeight;

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
        //woodCount = 0;
        //rockCount = 0;
        //grassCount = 0;
        highestHeight = 0;
        level = Application.loadedLevelName;

    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (highestHeight < GameObject.FindGameObjectWithTag("Player").transform.position.y)
            {
                highestHeight = (int)(GameObject.FindGameObjectWithTag("Player").transform.position.y);
            }
        }
            if (GameObject.FindGameObjectWithTag("Player") == false)
            {
                gameOverDisplay.text = "Game Over   Score: " + highestHeight;
                GOtime++;
            }
            if (GOtime >= 300)
            {
                Application.LoadLevel(level);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                
            }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("StartScene");
        }

        //Display Area
        woodDisplay.text = woodCount.ToString();
        rockDisplay.text = rockCount.ToString();
        grassDisplay.text = grassCount.ToString();
        score.text = "Score: " + highestHeight.ToString();

    }

    public void switchStructIcons(bool isPlatform)
    {
        float platPos = 50;
        float columnPos = 50;

        RectTransform wp;
        RectTransform rp;
        RectTransform gp;
        RectTransform wc;
        RectTransform rc;
        RectTransform gc;

        if (isPlatform) columnPos *= -1;
        else platPos *= -1;

        wp = transform.Find("woodPlatform").GetComponent<RectTransform>();
        rp = transform.Find("rockPlatform").GetComponent<RectTransform>();
        gp = transform.Find("grassPlatform").GetComponent<RectTransform>();
        wc = transform.Find("woodStruct").GetComponent<RectTransform>();
        rc = transform.Find("rockStruct").GetComponent<RectTransform>();
        gc = transform.Find("grassStruct").GetComponent<RectTransform>();

        wp.position = new Vector3(wp.position.x, platPos,0);
        rp.position = new Vector3(rp.position.x, platPos, 0);
        gp.position = new Vector3(gp.position.x, platPos, 0);
        wc.position = new Vector3(wc.position.x, columnPos, 0);
        rc.position = new Vector3(rc.position.x, columnPos, 0);
        gc.position = new Vector3(gc.position.x, columnPos, 0);

    }

}
