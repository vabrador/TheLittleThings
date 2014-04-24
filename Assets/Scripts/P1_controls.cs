using UnityEngine;
using System.Collections;

public class P1_controls : MonoBehaviour {
	MovementAnimationScript mover;
	// Use this for initialization
	void Start () {
		GameObject thisGuy = gameObject;
		mover = gameObject.GetComponent<MovementAnimationScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetAxis("P1 - Dash") > 0) && (!mover.stateBools["animating"])) {
			mover.Dash ();
		}
		else if ((Input.GetAxis("P1 - Block") > 0)  && (!mover.stateBools["animating"])) {
			mover.Block ();
		}
		else if ((Input.GetAxis("P1 - Punch") > 0) && (!mover.stateBools["animating"])) {
			mover.Punch ();
		}
		else {
			if (!mover.stateBools["animating"]){
				mover.fighterAnimation.CrossFade ("Idle");
			}
		}
	}
}
