using UnityEngine;
using System.Collections;
using SmoothMoves;

public class CollisionCatcherScript : MonoBehaviour {
	public string collisionSource;
	CombatScript parentCombat;
	MovementAnimationScript parentMover;

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (11, 11);
		Physics2D.IgnoreLayerCollision (12, 12);
		GameObject parent = gameObject.transform.root.gameObject;
		parentCombat = parent.GetComponent<CombatScript> ();
		parentMover = parent.GetComponent<MovementAnimationScript> ();
		parentMover.fighterAnimation.RegisterColliderTriggerDelegate (parentCombat.receiveCollision);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnCollisionEnter2D(Collision2D collision) {
//		Debug.Log ("We got a collision at " + gameObject);
//		CombatScript combat;
//		GameObject thisGuy = gameObject.transform.root.gameObject;
//		combat = thisGuy.GetComponent<CombatScript> ();
//		GameObject otherGuy = collision.gameObject.transform.root.gameObject;
////		Debug.Log ("The collidee ended up at " + thisGuy + ", the collider ended up at " + otherGuy);
//		if ((combat != null) && (otherGuy.GetComponent<CombatScript>() != null)) {
////			Debug.Log ("A collision happened between " + thisGuy + " and " + otherGuy + ", and it's going to the CombatScripts!");
//			combat.receiveCollision (collision);
//		}
//		else {
////			Debug.Log ("A collision happened between " + thisGuy + " and " + otherGuy + ", but they didn't have CombatScripts.");
//		}
//	}
}
