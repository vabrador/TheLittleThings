using UnityEngine;
using System.Collections;

public class CombatScript : MonoBehaviour {
	// Amount of damage done by punch
	public int punchStrength = 10;
	public int counterStrength = 5;

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

	// Function which CollisionCatcherScript passes collisions to.  Handles the logic of what
	// should happen based on the respective state and start times.  Specifically deals with
	// activating the hurt or countered states and subtracting health.
	//
	// Note that since each fighter will have its own version of the script running and every
	// collision between them will be mirrored (i.e. if the left fighter hits the right fighter
	// they'll both receive a collision), the script only dictates effects on this gameObject.
	public void receiveCollision(Collision2D collision, string source) {
		GameObject otherGuy = collision.gameObject;
		MovementAnimationScript otherMover = otherGuy.GetComponent<MovementAnimationScript> ();
		CombatScript otherCombat = otherGuy.GetComponent<CombatScript> ();
		if (collision.gameObject.tag == "fighter") {
			if (!mover.stateBools["animating"]){ // if this character is currently idle
				if (otherMover.stateBools["punching"]){
					GetsHurt(otherCombat.punchStrength);
				}
				else if (otherMover.stateBools["dashing"]){
					GetsKnocked();
				}
				else {}
			}
			else if (mover.stateBools["punching"]){
				if (otherMover.stateBools["punching"]) {
					if (mover.punchStart > otherMover.punchStart) { GetsHurt(otherCombat.punchStrength); }
					else {}
				}
				else if (otherMover.stateBools["dashing"]) { // If they're dashing when punched
					if (!otherCombat.dashCounterable) { GetsKnocked(); }
					else {}
				} 
				else if (otherMover.stateBools["blocking"]) { // If they're blocking when punched

				} 
				else if (!otherMover.stateBools["animating"]) {} // What happens if they're idle when you punch them
				else {} // What happens if hurt or countered
			}
			else if (mover.stateBools["blocking"]) {
				if (otherMover.stateBools["punching"]){}
				else if (otherMover.stateBools["dashing"]) {}
				else {}
			}
			else if (mover.stateBools["dashing"]) {
				if (otherMover.stateBools["punching"]){}
				else if (otherMover.stateBools["blocking"]){}
				else if (otherMover.stateBools["dashing"]){}
				else if (!otherMover.stateBools["animating"]){}
			}
			else {
			// Do nothing, because our character is either hurt or reeling, and in both
			// cases are safe from being hit or moved anymore than they already are.
			}
			
		}
	}


	void TakeDamage(int damageAmount) {
		currentHealth -= damageAmount;
		if (currentHealth <= 0) {
			mover.Die();
		}
	}

	void GetsKnocked() {
		mover.Reel ();
	}
	
	void GetsCountered(int counterStrength) {
		TakeDamage (counterStrength);
		mover.Reel ();
	}

	void GetsHurt(int damageAmount) {
		TakeDamage (damageAmount);
		mover.Hurt ();
	}

}
