using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Damage() {
		Debug.Log (this.gameObject + " damaged, destroying");
		Destroy (this.gameObject);
	}
}
