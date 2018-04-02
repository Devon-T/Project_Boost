using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Debug.isDebugBuild)
        {
            debugControls();
        }
    }

    private static void debugControls()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (Input.GetKey(KeyCode.L))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKey(KeyCode.K))
        {
            SceneManager.LoadScene(currentScene - 1);
        }
    }
}
