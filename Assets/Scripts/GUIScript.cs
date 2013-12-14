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
	}
}
