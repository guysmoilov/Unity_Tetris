using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	
	public Texture previewBoxTexture;

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
	}
}
