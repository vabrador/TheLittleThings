using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public int startHealth = 100;
	int currentHealth;

	// Use this for initialization
	void Start () {
		currentHealth = startHealth;
	}
	
//	// Update is called once per frame
//	void Update () {
//		
//	}

	void TakeDamage(int damageValue) {
		currentHealth -= damageValue;
		if (currentHealth <= 0) {
			MovementAnimationScript moveScript = GetComponent<MovementAnimationScript> ();
			moveScript.Die ();		
		}

	}
}
