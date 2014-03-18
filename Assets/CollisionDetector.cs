using UnityEngine;
using System.Collections;

public class CollisionDetector : MonoBehaviour {

	public ArrayList collisions = new ArrayList();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		collisions.Add(collision.collider.gameObject);
	}
	
	void OnCollisionExit2D(Collision2D collision) {
		collisions.Remove(collision.collider.gameObject);
	}
	
	public bool IsColliding() {
		return collisions.Count > 0;
	}
}
