using UnityEngine;
using System.Collections;

public class CollisionCatcherScript : MonoBehaviour {
	public string collisionSource;
	CombatScript parentCombat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		CombatScript combat;
		Debug.Log (collisionSource + " just collided with " + collision.collider + "!");
		if (gameObject.GetComponent<CombatScript> () == null){
			GameObject parentObject = gameObject.transform.parent.gameObject;
			while (parentObject.GetComponent<CombatScript>() == null) 
			{
				parentObject = gameObject.transform.parent.gameObject;
				Debug.Log ("A collision is propagating up from " + gameObject + "and it just got to " + parentObject);
			}
			combat = parentObject.GetComponent<CombatScript>();
		} else {
			combat = gameObject.GetComponent<CombatScript>();
		}
		combat.receiveCollision (collision, collisionSource);
	}
}
