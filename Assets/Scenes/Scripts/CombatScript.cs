﻿using UnityEngine;
using System.Collections;
using SmoothMoves;

public class CombatScript : MonoBehaviour {
	// Reference to the MovementAnimationScript on this GameObject.
	MovementAnimationScript mover;
	
	// Points used to calculate Special damage
	public Transform centerPoint;
	public Transform edgePoint;

	// Bool to turn on auto-damage function
	public bool autodamage;

	// Start time
	public float startTime;
	
	// Amount of damage done by punch, counter, special
	public int attackStrength = 10;
	public int counterStrength = 5;
	public int maxSpecialStrength = 30;
	
	public int centerDistance {
		get { return (int) (gameObject.transform.position.x - centerPoint.position.x); }
	}
	
	public int specialStrength {
		get { 
			int absDamage = (int) Mathf.Abs(((centerDistance)/(edgePoint.position.x - centerPoint.position.x))) * maxSpecialStrength;
			if ((centerDistance > 0 && mover.facingLeft) || (centerDistance < 0 && !mover.facingLeft)) { return absDamage; }
			else { return 0; }
		}
	}
	
	
	// Threshold amount of time after a move is begun when the opponent
	// can still counter it.
	public float attackThresh = 0.5f;
	public float blockThresh = 0.5f;
	public float dashThresh = 1f;
	
	// Health info
	public int startHealth = 100;
	public int currentHealth;
	
	// Properties to check whether the move is in the "counterable" zone
	bool punchCounterable {
		get { return (Time.time - mover.attackStart) < attackThresh; }
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
		startTime = Time.time;
		mover = GetComponent<MovementAnimationScript> ();
		mover.fighterAnimation.RegisterColliderTriggerDelegate (receiveColliderTriggerEvent);
	}
	
	// Update is called once per frame
	void Update () {
		AutoDamageChar ();
		//currentHealth--;
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		//Debug.Log ("There was just a collision between " + gameObject + " and " + collision.collider);
		if (collision.gameObject.tag == "fighter") {
			
		}
	}
	
	// Handles the logic of what should happen based on the respective state and start times.  Specifically deals with
	// activating the hurt or countered states and subtracting health.
	//
	// Note that since each fighter will have its own version of the script running and every
	// collision between them will be mirrored (i.e. if the left fighter hits the right fighter
	// they'll both receive a collision), the script only dictates effects on this gameObject.


	public void performCollisionResponse(GameObject otherChar){
		MovementAnimationScript otherMover = otherChar.GetComponent<MovementAnimationScript> ();
		CombatScript otherCombat = otherChar.GetComponent<CombatScript> ();
		int otherCounter = otherCombat.counterStrength;
//		Debug.Log ("Collision happened: " + gameObject + "'s attackState: " + mover.stateBools["attacking"]);
//		Debug.Log (otherChar + "'s attackState: " + otherMover.stateBools["attacking"]);
			
		if (!mover.stateBools["animating"]){
			// If you're idle and you get punched, you're hurt.  If you're idle and dashed
			// into, you get knocked back.  Otherwise, nothing changes.
			if (otherMover.stateBools["attacking"]){ GetsHurt(otherCombat.attackStrength); }
			else if (otherMover.stateBools["dashing"]){ GetsKnocked(); }
			else if (otherMover.stateBools["specialing"]){ GetsHurt(otherCombat.specialStrength); }
			else {}
		} 
		else if (mover.stateBools["specialing"]) {
			// If you're in the process of a special attack, you're pretty much invulnerable.
			// Therefore, nothing happens to your character.
		}
		else if (mover.stateBools["attacking"]){
			// If you're attacking at the same time, first guy to throw the punch wins.  If you
			// throw a punch into a dash and it *isn'* counterable, you get knocked back.  If
			// somebody blocks while your punch is counterable, you get countered.
			if ((otherMover.stateBools["attacking"]) && (mover.attackStart >= otherMover.attackStart)) { GetsHurt(otherCombat.attackStrength); }
			else if ((otherMover.stateBools["dashing"]) && (!otherCombat.dashCounterable)) { GetsKnocked(); }
			else if ((otherMover.stateBools["blocking"]) && (punchCounterable)) { GetsCountered(otherCounter); }
			else if (otherMover.stateBools["specialing"]){ GetsHurt(otherCombat.specialStrength); }
			else {}
		}
		else if (mover.stateBools["blocking"]) {
			// If you're blocking and get dashed into while you're counterable, you get countered.
			// Otherwise, not much happens.
			if ((otherMover.stateBools["dashing"]) && (blockCounterable)) { GetsCountered(otherCounter); }
			else if (otherMover.stateBools["specialing"]){ GetsHurt(otherCombat.specialStrength); }
			else {}
		}
		else if (mover.stateBools["dashing"]) {
			// If you're dashing into their punch and your dash is counterable, get countered.
			// Otherwise, no enemy state will changes yours mid-dash
			if ((otherMover.stateBools["attacking"]) && (dashCounterable)) { GetsCountered(otherCounter); }
			else if (otherMover.stateBools["specialing"]){ GetsHurt(otherCombat.specialStrength); }
			else{}
		}
		else {
			// Do nothing, because our character is either hurt or reeling, and in both
			// cases are safe from being hit or moved anymore than they already are.
		}
	}


	//////////////////////////////////////////////////
	////// FUNCTIONS TO CATCH COLLISION EVENTS ///////
	//////////////////////////////////////////////////

	// Function which CollisionCatcherScript passes colliderTriggerEvents to.  
	public void receiveColliderTriggerEvent(ColliderTriggerEvent colliderTriggerEvent) {
		GameObject otherChar = colliderTriggerEvent.otherCollider.gameObject.transform.root.gameObject;
		performCollisionResponse (otherChar);
	}

	// Function which CollisionCatcherScript passes collisions to.  
	public void receiveCollision(Collision2D collision) {
		GameObject otherChar = collision.gameObject.transform.root.gameObject;
		performCollisionResponse (otherChar);
	}


	//////////////////////////////////////////////////
	// FUNCTIONS TO CHANGE HEALTH & VARY ANIMATIONS //
	//////////////////////////////////////////////////

	// Subtracts from the health and triggers whatever happens upon Death
	void TakeDamage(int damageAmount) {
		Debug.Log (gameObject + " just lost " + damageAmount + " health!");
		currentHealth -= damageAmount;
		if (currentHealth <= 0) {
			mover.Die();
		}
	}
	
	// Used when a character gets pushed back by a Dash
	void GetsKnocked() {
		Debug.Log (gameObject + " just got knocked back!");
		mover.Reel ();
	}
	
	// Used when a character is countered -- calls Reel & Damage
	void GetsCountered(int counterStrength) {
		Debug.Log (gameObject + " just got countered!");
		TakeDamage (counterStrength);
		mover.Reel ();
	}
	
	// Used when damage is taken -- calls TakeDamage and the actual Hurt animation
	void GetsHurt(int damageAmount) {
		Debug.Log (gameObject + " triggered the GetsHurt function!");
		TakeDamage (damageAmount);
		mover.Hurt ();
	}

	void AutoDamageChar(){
		if ((autodamage) && ((Time.time - startTime) % 3 < 1)) TakeDamage(5);
	}
	
}