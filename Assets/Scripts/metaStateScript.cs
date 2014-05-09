using UnityEngine;
using System.Collections;

public class metaStateScript : MonoBehaviour {
	public GameObject leftChar;
	public GameObject rightChar;
	PlayerControl leftControl;
	PlayerControl rightControl;
	// Use this for initialization
	void Start () {
		leftControl = leftChar.GetComponent<PlayerControl>();
		rightControl = rightChar.GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void endGame(bool win) {
		leftControl.gameRunning = false;
		rightControl.gameRunning = false;
		if (win) {
			GameObject winGraphic = (GameObject) Instantiate(Resources.Load ("lindaWinGraphic"));
		} else {
			GameObject winGraphic = (GameObject) Instantiate(Resources.Load ("georgeWinGraphic"));
		}
	}
}
