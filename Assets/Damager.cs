using UnityEngine;
using System.Collections;

public class Damager : MonoBehaviour {
	
	public ArrayList overlappingColliders = new ArrayList();
	public ArrayList deletionBuffer = new ArrayList();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		overlappingColliders.Add(collider);
	}
	
	void OnTriggerExit2D(Collider2D collider) {
		overlappingColliders.Remove(collider);
	}

	public void Damage() {
		foreach (Collider2D collider in overlappingColliders) {
			// First check if the collider we made reference to still exists
			// because it might have been destroyed (by damage!)
			if (collider == null) {
				// This object no longer exists so schedule it for deletion
				deletionBuffer.Add(collider);
			}
			else {
				Damageable toDamage = collider.gameObject.GetComponent<Damageable>();
				if (toDamage != null) {
					toDamage.Damage();
				}
			}
		}
		// cleanup; get rid of any deleted colliders
		foreach (Collider2D collider in deletionBuffer) {
			overlappingColliders.Remove(collider);
		}
		// Finally, clear the deletionBuffer
		deletionBuffer.Clear ();
	}
}
