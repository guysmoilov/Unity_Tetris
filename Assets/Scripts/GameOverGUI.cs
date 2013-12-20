using UnityEngine;
using System.Collections;

public class GameOverGUI : MonoBehaviour {

	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width / 3, Screen.height / 6, Screen.width / 3, 20), "Game over!");
		GUI.Label(new Rect(Screen.width / 3, Screen.height / 6 + 20, Screen.width / 3, 20), "Your score: " + BoardScript.score);

		if(GUI.Button(new Rect(Screen.width / 3, Screen.height / 2, Screen.width / 3, Screen.height / 6), "Back to main menu"))
		{
			Application.LoadLevel("Menu");
		}
	}
}
