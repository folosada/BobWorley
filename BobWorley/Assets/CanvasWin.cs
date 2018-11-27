using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWin : MonoBehaviour {

    public Button exit;

    // Use this for initialization
    void Start () {        
        exit.onClick.AddListener(exitGame);
	}    

    private void exitGame()
    {
        Application.Quit();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
