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
		Debug.Log (collisionSource + " got into a collision!");
		CombatScript combat;
		GameObject thisGuy = GetToParent (gameObject);
		combat = thisGuy.GetComponent<CombatScript> ();
		GameObject otherGuy = GetToParent (collision.gameObject);
//		Debug.Log ("The collidee ended up at " + thisGuy + ", the collider ended up at " + otherGuy);
		if ((combat != null) && (otherGuy.GetComponent<CombatScript>() != null)) {
//			Debug.Log ("A collision happened between " + thisGuy + " and " + otherGuy + ", and it's going to the CombatScripts!");
			combat.receiveCollision (otherGuy);
		}
		else {
//			Debug.Log ("A collision happened between " + thisGuy + " and " + otherGuy + ", but they didn't have CombatScripts.");
		}
	}

	GameObject GetToParent(GameObject child) {
		GameObject originalObject = child;
		GameObject parent = child;
		try {
			while (parent.transform.parent.gameObject != null) {
				parent = parent.transform.parent.gameObject;
			}
			return parent; // This line will never get used because we're supposed to always step to null, but if it isn't there Unity bitches.
		} catch (System.NullReferenceException ex) {
			return parent;
		}
		}
	}
