using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {
	
	public GUISkin skin;
	public Texture screenTexture;
	public Texture startTexture;
	
	public void OnGUI() {
		GUI.skin = skin;
		float startButtonX = Screen.width * 27 / 40;
		float startButtonY = Screen.height / 5;
		float startButtonWidth = 186;
		float startButtonHeight = 82;

		GUI.Label(new Rect(0, 0, Screen.width, Screen.height), screenTexture);
		if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), startTexture)) {
			Application.LoadLevel("TheBeginning");
		}
	}	
}