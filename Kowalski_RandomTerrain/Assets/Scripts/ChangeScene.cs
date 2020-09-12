using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour 
{
	public string sceneName;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown("c"))
        {
			SceneManager.LoadScene(sceneName);
        }
	}

	void OnGUI ()
    {
		GUI.Box(new Rect(Screen.width - 130, 10, 120, 50), "Press 'c' key to switch to " + sceneName);
		GUI.skin.box.wordWrap = true;
	}
}
