using UnityEngine;
using System.Collections;
using SmoothMoves;

public class FighterScript : MonoBehaviour {

	public BoneAnimation fighterAnimation;
	public float maxSpeed = 10f;
	bool facingLeft;
	bool animating;
	public string characterSide = "left";

	// Booleans to track animation state.
	bool punching = false;
	bool blocking = false;
	bool dashing = false;

	public float dashLength = 2f;
	public float punchLength = 1f;
	public float blockLength = 2f;

	// Variables to keep track of when the most recent move
	// of each type started.
	float punchStart = 0f;
	float blockStart = 0f;
	float dashStart = 0f;

	// Use this for initialization
	void Start () {
		animating = false;
		if (characterSide == "right")
				facingLeft = true;
		else
				facingLeft = false;
		if (!facingLeft)
			Flip ();
	}

	// Update is called once per frame
	void FixedUpdate() {
		// Set character speed on dash and set the move booleans, 
		// i.e. are you dashing, punching, blocking?
		if (fighterAnimation.IsPlaying("Dash")) {
			dashing = true;
			if (!facingLeft)
				rigidbody2D.velocity = new Vector2(maxSpeed, 0);
			else
				rigidbody2D.velocity = new Vector2(-1 * maxSpeed, 0);
		} else if (fighterAnimation.IsPlaying("Punch")) {
			punching = true;
		} else if (fighterAnimation.IsPlaying("Block")) {
			blocking = true;
		} else {
			animating = false;
			dashing = false;
			punching = false;
			blocking = false;
		}

		// When the animation has gone long enough, smoothly crossfade to idle
		if ((dashing && (Time.time - dashStart > dashLength)) ||
			(blocking && (Time.time - blockStart > blockLength)) ||
			(punching && (Time.time - punchStart > punchLength)))
		{
			fighterAnimation.CrossFade("Idle");	
		}
	}

	// Update is called whenever an Input is grabbed
	void Update () {
		if ((Input.GetAxis("Dash") > 0) && (!animating)) {
			animating = true;
			fighterAnimation.CrossFade ("Dash");
			dashStart = Time.time;
		}
		else if ((Input.GetAxis("Block") > 0)  && (!animating)) {
			animating = true;
			fighterAnimation.CrossFade ("Block");
			blockStart = Time.time;

		}
		else if ((Input.GetAxis("Punch") > 0) && (!animating)) {
			animating = true;
			fighterAnimation.Play ("Punch");
			punchStart = Time.time;
		}
		else {
			if (!animating) {
				fighterAnimation.CrossFade ("Idle");
			}
		}
	
	}


	void Flip() {
		Vector3 localScale = transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}
}
