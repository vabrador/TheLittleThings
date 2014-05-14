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
		Vector3 currentCenter = Camera.main.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0f));
		currentCenter.z = leftChar.transform.position.z;
		if (win) {
			GameObject winGraphic = (GameObject) Instantiate(Resources.Load ("lindaWinGraphic"), currentCenter, Quaternion.identity);
//			winGraphic.transform.position.x = new VecurrentCenter.x;
		} else {
			GameObject loseGraphic = (GameObject) Instantiate(Resources.Load ("georgeWinGraphic"), currentCenter, Quaternion.identity);
//			loseGraphic.transform.position.x = currentCenter.x;
		}
	}
}
