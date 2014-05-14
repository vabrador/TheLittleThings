using UnityEngine;
using System.Collections;

public class SolidProjectile : MonoBehaviour {
	public float speed = 10f;
	public GameObject projectileCreator;
	float currentForce = 0f;
	float startTime;

	// Use this for initialization
	void Start () {
		currentForce = speed * 10000;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log ("Adding a force of " + currentForce + " to " + gameObject);
//		rigidbody2D.velocity = new Vector2(currentForce, 0f);
		rigidbody2D.AddForce ( new Vector2( (Time.time - startTime) * currentForce, 0 ));
		Debug.Log ("Resulting in a velocity of " + rigidbody2D.velocity);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		currentForce = 0;
		Debug.Log ("DEL stopped by " + collision.gameObject);
		GameObject.Destroy (gameObject);
	}
}
