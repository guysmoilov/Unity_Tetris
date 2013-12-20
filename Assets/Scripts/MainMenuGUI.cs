using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

	public string title = "Guy's tetris";

	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width / 3, Screen.height / 6, Screen.width / 3, Screen.height / 6), title);

		if(GUI.Button(new Rect(Screen.width / 3, Screen.height / 2, Screen.width / 3, Screen.height / 6), "Start game"))
		{
			Application.LoadLevel("Main");
		}

		if(GUI.Button(new Rect(Screen.width / 3, Screen.height * (2f/3), Screen.width / 3, Screen.height / 6), "Quit"))
		{
			Application.Quit();
		}
	}
}
