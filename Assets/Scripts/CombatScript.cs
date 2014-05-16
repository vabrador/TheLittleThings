using UnityEngine;
using System.Collections;
using SmoothMoves;

public class CombatScript : MonoBehaviour {
	// Reference to the MovementAnimationScript on this GameObject.
	MovementAnimationScript mover;
	ComboManager combo;
	ComboManager otherCombo;

    // Reference to the OTHER VocalFighter for success sounds
    public VocalFighter otherVocalFighter;
    // Reference to THIS VocalFighter for success sounds
    public VocalFighter thisVocalFighter;
	
	// Points used to calculate Special damage
	public Transform centerPoint;
	public Transform leftEdgePoint;
	public Transform rightEdgePoint;

	// Bool to turn on auto-damage function
	public bool autodamage;

	// Start time
	public float startTime;
	
	// Amount of damage done by punch, counter, special
	public int attackStrength = 10;
	public int counterStrength = 5;
	public int maxSpecialStrength = 60;
	
	public float centerDistance {
		get { return (gameObject.transform.position.x - centerPoint.position.x); }
	}
	
	public int specialStrength {
		get { 
			float centerPos = centerPoint.position.x;
			float edgePos = leftEdgePoint.position.x;
			float playerPos = gameObject.transform.position.x;
			int absDamage = (int) Mathf.Abs(centerDistance / (centerPos - edgePos) * maxSpecialStrength);
//			if (Time.time % 3 < 0.01) Debug.Log ("Damage should be scaled as: "+ (centerDistance / (centerPos - edgePos)) * maxSpecialStrength);
//			if (Time.time % 5 < 0.01) Debug.Log ("absDamage is: " + absDamage);
			if (((playerPos < centerPos) && mover.facingLeft) || ((playerPos > centerPos) && !mover.facingLeft)) { return absDamage; }
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
		ComboManager[] comboManagers = GameObject.Find ("God").GetComponents<ComboManager> ();
		foreach(ComboManager comboMan in comboManagers) {
			if (comboMan.playerChar == gameObject) {
				combo = comboMan;
			} else {
				otherCombo = comboMan;
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		AutoDamageChar ();
//		if (Time.time %3 < 0.01) Debug.Log (gameObject + "'s special strength is: "+ specialStrength);
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
		Debug.Log ("Collision from " + otherChar + "being handled by: " + gameObject);
		if (mover.stateBools["idling"]){
			// If you're idle and you get punched, you're hurt.  If you're idle and dashed
			// into, you get knocked back.  Otherwise, nothing changes.
			if (otherMover.stateBools["attacking"]){ GetsHurt(otherCombat.attackStrength); }
			else if (otherMover.stateBools["dashing"]){ 
//				otherMover.Bounce();
				GetsKnocked(); 
			}
			else if (otherMover.stateBools["specialing"]){ GetsHurt(otherCombat.specialStrength); }
			else {}
		} 
		else if (mover.stateBools["specialing"]) {
			// If you're in the process of a special attack, you're pretty much invulnerable.
			// Therefore, all that happens is one more point to your combo counter.
		}
		else if (mover.stateBools["attacking"]){
			// If you're attacking at the same time, first guy to throw the punch wins.  If you
			// throw a punch into a dash and it *isn'* counterable, you get knocked back.  If
			// somebody blocks while your punch is counterable, you get countered.
			if ((otherMover.stateBools["attacking"]) && (mover.attackStart >= otherMover.attackStart)) {
				GetsHurt(otherCombat.attackStrength); 
			}
			else if (otherMover.stateBools["dashing"]) {
				if (!otherCombat.dashCounterable) { GetsKnocked(); }
			}
			else if ((otherMover.stateBools["blocking"]) && (punchCounterable)) { GetsCountered(otherCounter); }
			else if (otherMover.stateBools["specialing"]){ GetsHurt(otherCombat.specialStrength); }
			else {}
		}
		else if (mover.stateBools["blocking"]) {
			// If you're blocking and get dashed into while you're counterable, you get countered.
			// Otherwise, not much happens.
			if ((otherMover.stateBools["dashing"]) && (blockCounterable)) { 
//				otherMover.Bounce();
				GetsCountered(otherCounter);
                otherVocalFighter.OnDashSuccess();
                thisVocalFighter.OnBlockBroken();
			}
            else if (otherMover.stateBools["blocking"]) {
                thisVocalFighter.OnBlockSuccess();
            }
            else if (otherMover.stateBools["specialing"]) { GetsHurt(otherCombat.specialStrength); }
            else { }
		}
		else if (mover.stateBools["dashing"]) {
			// If you're dashing into their punch and your dash is counterable, get countered.
			// Otherwise, no enemy state will changes yours mid-dash
			if (otherMover.stateBools["attacking"]) {
				if (dashCounterable) {
					GetsCountered(otherCounter); 
				}
			}
			else if (otherMover.stateBools["specialing"]){ GetsHurt(otherCombat.specialStrength); }
			else{ otherMover.Idle(); }
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
//		Debug.Log (gameObject + " just received a ColliderTriggerEvent from " + otherChar);
		performCollisionResponse (otherChar);
	}

	// Function which CollisionCatcherScript passes collisions to.  
	public void receiveCollision(Collision2D collision) {
		GameObject otherChar = collision.gameObject.transform.root.gameObject;
//		Debug.Log (gameObject + " just received a collision from " + otherChar);
		performCollisionResponse (otherChar);
	}


	//////////////////////////////////////////////////
	// FUNCTIONS TO CHANGE HEALTH & ANIMATIONS ///////
	//////////////////////////////////////////////////

	// Subtracts from the health and triggers whatever happens upon Death.
	// Also tells the character's ComboManager that the character just took
	// damage and should have its comboCount reset.
	void TakeDamage(int damageAmount) {
//		Debug.Log (gameObject + " just lost " + damageAmount + " health!");
		currentHealth -= damageAmount;
		combo.hitTaken ();
		otherCombo.hitLanded ();
		if (currentHealth <= 0) {
			currentHealth = 0;
			mover.EndGame();
		}
	}
	
	// Used when a character gets pushed back by a Dash
	void GetsKnocked() {
		Debug.Log (gameObject + " just got knocked back!");
		mover.Reel ();
        otherVocalFighter.OnCounterSuccess();
	}
	
	// Used when a character is countered -- calls Reel & Damage
	void GetsCountered(int counterStrength) {
		Debug.Log (gameObject + " just got countered!");
		TakeDamage (counterStrength);
		mover.Reel ();
        otherVocalFighter.OnCounterSuccess();
	}
	
	// Used when damage is taken -- calls TakeDamage and the actual Hurt animation
	void GetsHurt(int damageAmount) {
		Debug.Log (gameObject + " got hurt!");
		TakeDamage (damageAmount);
		mover.Hurt ();
        otherVocalFighter.OnAttackSuccess();
	}

	void AutoDamageChar(){
		if ((autodamage) && ((Time.time - startTime) % 3 < 0.01)) TakeDamage(5);
	}
	
}
