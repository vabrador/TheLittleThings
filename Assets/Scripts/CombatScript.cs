using UnityEngine;
using System.Collections;

public class CombatScript : MonoBehaviour {
	// Amount of damage done by punch
	public int punchStrength = 10;

	// Threshold amount of time after a move is begun when the opponent
	// can still counter it.
	public float punchThresh = 0.5f;
	public float blockThresh = 0.5f;
	public float dashThresh = 1f;

	// Reference to the MovementAnimationScript on this GameObject.
	MovementAnimationScript mover;

	// Health info
	public int startHealth = 100;
	int currentHealth;

	// Properties to check whether the move is in the "counterable" zone
	bool punchCounterable {
		get { return (Time.time - mover.punchStart) < punchThresh; }
	}
	bool blockCounterable {
		get { return (Time.time - mover.blockStart) < blockThresh; }
	}
	bool dashCounterable{
		get { return (Time.time - mover.dashStart) < dashThresh; }
	}

	
	// Use this for initialization
	void Start () {
		currentHealth = startHealth;
		mover = GetComponent<MovementAnimationScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		//Debug.Log ("There was just a collision between " + gameObject + " and " + collision.collider);
		if (collision.gameObject.tag == "fighter") {
			
		}
	}

	public void receiveCollision(Collision2D collision, string source) {
		Debug.Log ("A collision happened and then got up to here!");
		Debug.Log ("It came from " + source);
	}

	void TakePunch(Collision2D collision) {
		GameObject otherFighter = collision.gameObject;
		CombatScript otherCombat = otherFighter.GetComponent<CombatScript> ();
		if (!mover.stateBools["blocking"]) {
			currentHealth -= otherCombat.punchStrength;
			mover.Hurt();
		} else {

		}
	}


}
