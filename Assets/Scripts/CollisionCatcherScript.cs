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
		if ((parentCombat != null) && (otherChar.GetComponent<CombatScript>() != null)) {
			parentCombat.receiveCollision (collision);
		}
	}
}
