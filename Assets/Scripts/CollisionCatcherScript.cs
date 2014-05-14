using UnityEngine;
using System.Collections;
using SmoothMoves;

public class CollisionCatcherScript : MonoBehaviour {
	public string characterName = "Linda Left";
	GameObject parentChar;
	CombatScript parentCombat;
//	MovementAnimationScript parentMover;

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (11, 11);
		Physics2D.IgnoreLayerCollision (12, 12);
		parentChar = GameObject.Find (characterName);
		parentCombat = parentChar.GetComponent<CombatScript> ();
//		parentMover = parentChar.GetComponent<MovementAnimationScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D collision) {
		GameObject otherChar = collision.gameObject.transform.root.gameObject;
//		Debug.Log ("The collidee ended up at " + thisGuy + ", the collider ended up at " + otherGuy);
		if ((parentCombat != null) && (otherChar.GetComponent<CombatScript>() != null)) {
//			Debug.Log ("A collision happened between " + thisGuy + " and " + otherGuy + ", and it's going to the CombatScripts!");
//			otherChar.GetComponent<CombatScript>().receiveCollision (collision);
			parentCombat.receiveCollision (collision);
		}
		else {
//			Debug.Log ("A collision happened between " + thisGuy + " and " + otherGuy + ", but they didn't have CombatScripts.");
		}
	}
}
