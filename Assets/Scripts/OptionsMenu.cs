using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    public Button easy;
    public Button normal;
    public Button hard;
    public Button freeMode;
    public Button puzzleMode;
    public Button goBack;

	// Use this for initialization
	void Start () {
        easy.gameObject.SetActive(false);
        normal.gameObject.SetActive(false);
        hard.gameObject.SetActive(false);
        goBack.gameObject.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void openMenu()
    {
        easy.gameObject.SetActive(true);
        normal.gameObject.SetActive(true);
        hard.gameObject.SetActive(true);
        goBack.gameObject.SetActive(true);
        freeMode.gameObject.SetActive(false);
        puzzleMode.gameObject.SetActive(false);
    }
    public void backToStart()
    {
        easy.gameObject.SetActive(false);
        normal.gameObject.SetActive(false);
        hard.gameObject.SetActive(false);
        goBack.gameObject.SetActive(false);
        freeMode.gameObject.SetActive(true);
        puzzleMode.gameObject.SetActive(true);
    }
}
