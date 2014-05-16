using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	MovementAnimationScript mover;
	public string[] friendlyAxes;
	public string[] enemyAxes;
	public bool gameRunning = true;
	public float buttonQuarantineLength = 0.25f;
	float lastPress;
	bool friendly;

	// Use this for initialization
	void Start () {
		mover = gameObject.GetComponent<MovementAnimationScript> ();
		friendly = !mover.enemy;
		friendlyAxes = new string[4] {"P1 - Dash", "P1 - Block", "P1 - Punch", "P1 - Special"};
		enemyAxes = new string[4] {"P2 - Dash", "P2 - Block", "P2 - Punch", "P2 - Special"};
	}
	
	// Update is called once per frame
	void Update () {
		if ((gameRunning) && (Time.time - lastPress > buttonQuarantineLength)) {
			string[] controls;
			if (friendly) {controls = friendlyAxes; }
			else {controls = enemyAxes; }
			if (!(mover.stateBools["hurting"] || mover.stateBools["reeling"] || mover.stateBools["specialing"])){
				if (Input.GetAxis(controls[0]) > 0) {
					mover.Dash ();
					lastPress = Time.time;
				}
				else if (Input.GetAxis(controls[1]) > 0) {
					mover.Block ();
					lastPress = Time.time;
				}
				else if (Input.GetAxis(controls[2]) > 0) {
					mover.Punch ();
					lastPress = Time.time;
				}
				else if (Input.GetAxis(controls[3]) > 0) {
					mover.Special();
					lastPress = Time.time;
				}
			}

		}
	}
}


