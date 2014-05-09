using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	MovementAnimationScript mover;
	public bool friendly;
	public string[] friendlyAxes;
	public string[] enemyAxes;
	public bool gameRunning = true;

	// Use this for initialization
	void Start () {
		mover = gameObject.GetComponent<MovementAnimationScript> ();
		friendlyAxes = new string[4] {"P1 - Dash", "P1 - Block", "P1 - Punch", "P1 - Special"};
		enemyAxes = new string[4] {"P2 - Dash", "P2 - Block", "P2 - Punch", "P2 - Special"};
	}
	
	// Update is called once per frame
	void Update () {
		if (gameRunning) {
			string[] controls;
			if (friendly) {controls = friendlyAxes; }
			else {controls = enemyAxes; }

			if ((Input.GetAxis(controls[0]) > 0) && (mover.stateBools["idling"])) {
				mover.Dash ();
			}
			else if ((Input.GetAxis(controls[1]) > 0)  && (mover.stateBools["idling"])) {
				mover.Block ();
			}
			else if ((Input.GetAxis(controls[2]) > 0) && (mover.stateBools["idling"])) {
				mover.Punch ();
			}
			else if ((Input.GetAxis(controls[3]) > 0) && (mover.stateBools["idling"])) {
				mover.Special();
			}
		}
	}
}


