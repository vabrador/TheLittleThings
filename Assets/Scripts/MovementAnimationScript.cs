using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SmoothMoves;

public class MovementAnimationScript : MonoBehaviour {
	// fighterAnimation is our animator
	public BoneAnimation fighterAnimation;
	public float maxSpeed = 10f;
	
	
	// characterSide & facingLeft help determine fighter orientation in code.
	public string characterSide = "left";
	public bool facingLeft {
		get{ return characterSide == "right"; }
	}
	
	// Corrects the direction of max velocity for what side a fighter is on
	float fighterMaxSpeed {
		get { 
			float currentSpeed = ( 1 - (Time.time - attackStart)/(attackLength) ) * maxSpeed;
//			Debug.Log ("currentSpeed with no transform = " + currentSpeed);
			if (!facingLeft){ 
				currentSpeed *= -1;
//				Debug.Log ("Saying " + fighterAnimation + " speed should be " + currentSpeed);
			}
			return currentSpeed;
		}
	}
	
	// Booleans to track animation state -- punchling, blocking,
	// dashing, hurting (just got hit), or reeling.  In general,
	// all animations are states we keep track of.
	public Dictionary<string, bool> stateBools = new Dictionary<string, bool>()
	{
		{"animating", false},
		{"attacking", false},
		{"blocking", false},
		{"dashing" , false},
		{"specialing", false},
		{"hurting", false},
		{"reeling", false}
	};
	// A list of the state keys so we can iterate through the entries in the
	// dictionary without iterating through the dictionary itself.
	public List<string> stateList = new List<string>();
	
	// Animations are of odd lengths and can't be easily changed, so these variables
	// manually set their lengths and let us change them after the fact.
	public float attackLength = 1f;
	public float dashLength = 2f;
	public float blockLength = 2f;
	public float hurtLength = 0.5f;
	public float reelLength = 2f;
	public float specialLength = 3f;
	
	// Variables to keep track of when the most recent move
	// of each type started.
	public float attackStart = 0f;
	public float blockStart = 0f;
	public float dashStart = 0f;
	public float hurtStart = 0f;
	public float reelStart = 0f;
	public float specialStart = 0f;
	
	// Since animations are of weird lengths and can't be easily modified,
	// these properties calculate whether an animation should be "done"
	// using the start & length variables.
	bool attackDone {
		get{ return (Time.time - attackStart) < attackLength;  }
	}
	bool dashDone {
		get{ return (Time.time - dashStart) < dashLength;  }
	}
	bool blockDone {
		get{ return (Time.time - blockStart) < blockLength;  }
	}
	bool specialDone {
		get{ return (Time.time - specialStart) < specialLength; }
	}
	bool hurtDone {
		get{ return (Time.time - hurtStart) < hurtLength;  }
	}
	bool reelDone {
		get{ return (Time.time - reelStart) < reelLength;  }
	}
	
	// Use this for initialization
	void Start () {
		stateList = new List<string>(stateBools.Keys);
		stateBools["animating"] = false;
//		if (facingLeft)
//			Flip ();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		// Set all of the animation booleans in the dictionary based
		// on current animation.  Specifically, animating and the current
		// animation should be true, the rest should all be false.
		if (fighterAnimation.IsPlaying("Dash")) {
			makeOtherAnimsFalse("dashing");
			float currentSpeed = Mathf.Abs((1 - (Time.time - dashStart)/dashLength)) * fighterMaxSpeed;
			rigidbody2D.velocity = new Vector2(currentSpeed, 0);
//			Debug.Log(fighterAnimation + " moving with a speed of " + currentSpeed);
		} else if (fighterAnimation.IsPlaying ("Countered")) {
			makeOtherAnimsFalse("reeling");
		} else if (fighterAnimation.IsPlaying("Hurt1") || fighterAnimation.IsPlaying("Hurt2")) {
			makeOtherAnimsFalse("hurting");
		} else if (fighterAnimation.IsPlaying("Attack")) {
			makeOtherAnimsFalse("attacking");
		} else if (fighterAnimation.IsPlaying("Block")) {
			makeOtherAnimsFalse("blocking");
		} else if (fighterAnimation.IsPlaying("Special")) {
			makeOtherAnimsFalse("specialing");
		} else {
			foreach (var state in stateList) {
				stateBools[state] = false;
			}
		}
		
		// When any animation has gone long enough, smoothly crossfade to idle
		if (!((stateBools["dashing"] && dashDone) || (stateBools["blocking"] && blockDone) ||
		      (stateBools["reeling"] && reelDone) || (stateBools["attacking"] && attackDone) ||
		      (stateBools["hurting"] && hurtDone) || (stateBools["specialing"] && specialDone)))
		{
			fighterAnimation.Play("Idle"); 
		}
	}
	
	// Update is called whenever an Input is grabbed
	void Update () {
		
	}
	
	// Separate functions to trigger each conceptual move or state in the game,
	// separating functionality & allowing them to be called from other scripts.
	public void Dash() {
		fighterAnimation.Play("Dash");
//		rigidbody2D.velocity = new Vector2(fighterMaxSpeed, 0);
		dashStart = Time.time;
	}
	
	public void Block() {
		fighterAnimation.Play("Block");
		blockStart = Time.time;
	}
	
	public void Punch() {
		fighterAnimation.Play("Attack");
		attackStart = Time.time;
	}
	
	public void Special() {
		fighterAnimation.Play("Special");
		specialStart = Time.time;
	}
	
	public void Reel() {
		fighterAnimation.Play ("Countered");
		rigidbody2D.velocity = new Vector2 ((float) (-0.25 * fighterMaxSpeed), 0);
		reelStart = Time.time;
	}
	
	public void Hurt() {
		if (Random.value > 0.5) { fighterAnimation.Play("Hurt1"); }
		else { fighterAnimation.Play("Hurt2"); }
		hurtStart = Time.time;
	}
	
	public void Die() {
		fighterAnimation.CrossFade ("Death");
	}
	
	// Sets all values in stateBools to false, except for those of animating
	// and the key provided.  Essentially a helper method for the beginning of
	// FixedUpdate().
	void makeOtherAnimsFalse(string move){
		if (stateBools.ContainsKey(move)) {
			foreach(string state in stateList) {
				if ((state == move) || (state == "animating")) {stateBools[state] = true; }
				else {stateBools[state] = false; }
			}
		} else {
			throw new System.ArgumentException("The parameter given wasn't in stateBool.");
		}
	}
	
	
	// Helper method to invert the opposing identical character on the field.  Useful
	// while prototyping, but will be phased out once we have real art.
	void Flip() {
		Vector3 localScale = transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}
	
	
}
