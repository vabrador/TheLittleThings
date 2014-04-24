using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {
	
	public GUISkin skin;
	public float halfScreenWidth = Screen.width / 2;
	public float halfScreenHeight = Screen.height / 2;
	public float buttonWidth = 320;
	public float buttonHeight = 200;
	
	public void OnGUI() {
		GUI.skin = skin;
		float buttonX = halfScreenWidth - 160;
		float buttonY = halfScreenHeight * 6 / 10;
		if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Start Game")) {
			Application.LoadLevel("The Beginning");
		}
	}
	
}
