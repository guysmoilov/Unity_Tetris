using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	
	public Texture previewBoxTexture;

	private bool paused = false;

	void Update()
	{
		if (Input.GetButtonDown("Esc"))
		{
			if (!paused)
			{
				paused = true;
				Time.timeScale = 0;
			}
			else
			{
				paused = false;
				Time.timeScale = 1;
			}
		}

	}

	void OnGUI()
	{
		if (previewBoxTexture != null)
		{
			GUI.Box(new Rect(Screen.width - 10 - Screen.width / 3, 10, Screen.width / 3, Screen.height / 2), previewBoxTexture);
		}
		else
		{
			GUI.Box(new Rect(Screen.width - 10 - Screen.width / 3, 10, Screen.width / 3, Screen.height / 2), "Next:");
		}

		GUI.Label(new Rect(Screen.width - 10 - Screen.width / 3, Screen.height - 50, Screen.width / 3, 20), "Score: " + BoardScript.score);

		if (paused)
		{
			if (GUI.Button(new Rect(Screen.width * (1f/3), Screen.height / 2, Screen.width / 6, 40), "Quit"))
			{
				Application.Quit();
			}

			if (GUI.Button(new Rect(Screen.width * (1f/2), Screen.height / 2, Screen.width / 6, 40), "Resume"))
			{
				paused = false;
				Time.timeScale = 1;
			}
		}
	}
}
