using UnityEngine;
using System.Collections;

public class CollisionCatcherScript : MonoBehaviour {
	public string collisionSource;
	CombatScript parentCombat;

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (11, 11);
		Physics2D.IgnoreLayerCollision (12, 12);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		CombatScript combat;
		GameObject thisGuy = gameObject.transform.root.gameObject;
		combat = thisGuy.GetComponent<CombatScript> ();
		GameObject otherGuy = collision.gameObject.transform.root.gameObject;
//		Debug.Log ("The collidee ended up at " + thisGuy + ", the collider ended up at " + otherGuy);
		if ((combat != null) && (otherGuy.GetComponent<CombatScript>() != null)) {
//			Debug.Log ("A collision happened between " + thisGuy + " and " + otherGuy + ", and it's going to the CombatScripts!");
			combat.receiveCollision (otherGuy);
		}
		else {
//			Debug.Log ("A collision happened between " + thisGuy + " and " + otherGuy + ", but they didn't have CombatScripts.");
		}
	}
}
