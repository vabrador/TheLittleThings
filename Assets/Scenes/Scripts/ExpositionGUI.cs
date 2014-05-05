using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class ExpositionGUI : MonoBehaviour {

	public Texture2D backgroundImage;
	public string scriptFile;

	void onGUI() {
		List<string> scriptLines = GetScript (scriptFile);
		string scriptText;
		int currentLine = 0;
		GUI.BeginGroup (new Rect ((int)(Screen.width * 0.1), (int)(Screen.height * 0.65), 
		                          (int)(Screen.width * 0.9), (int)(Screen.height * 0.95)));
		GUI.Box (new Rect (0, 0, 100, 100), "Characters Text");
		scriptText = GUI.TextField (new Rect (5, 5, 95, 95), scriptLines[currentLine]);
		if (GUI.Button (new Rect (50, 0, 95, 5), "Next Line")) {
			currentLine ++;
		}
		GUI.EndGroup ();

	}

	private List<string> GetScript(string fileName) {
		try {
			StreamReader scriptReader = new StreamReader(scriptFile, Encoding.Default);
			using (scriptReader) {
				string fileLine;
				string characterLine = "";
				List<string> script = new List<string>();
				do {
					fileLine = scriptReader.ReadLine();
					if (fileLine != null) {
						if (fileLine != "\n") {
							characterLine = characterLine + fileLine;
						} else {
							script.Add(characterLine);
							characterLine = "";
						}
					}
				}
				while(fileLine != null);
				return script;
			}
		}
		catch (IOException e) {
			print(e.Message);
			List<string> dummyList = new List<string>();
			return dummyList;
		}
	}
}
