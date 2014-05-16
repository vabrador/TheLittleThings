using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {
	public GameObject leftCharacter;
	public GameObject rightCharacter;
	public Transform leftEdge;
	public Transform rightEdge;
	public Transform centerPoint;
	public Vector3 playerCenter;
	// Use this for initialization
	void Start () {
		leftEdge = GameObject.Find ("leftEdgePoint").transform;
		rightEdge = GameObject.Find ("rightEdgePoint").transform;
		centerPoint = GameObject.Find ("centerPoint").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Transform leftTransform = leftCharacter.transform;
		Transform rightTransform = rightCharacter.transform;
		float average = (leftTransform.position.x + rightTransform.position.x) / 2;
		Vector3 newPos;
//		if ((camera.ViewportToWorldPoint(new Vector3(0,0,0)).x + average <= -135) || (camera.ViewportToWorldPoint(new Vector3(1,0,0)).x > 142)) {
		if ( ((camera.ViewportToWorldPoint(new Vector3(0,0,0)).x <= leftEdge.position.x) && (average < camera.ViewportToWorldPoint(new Vector3((float)0.5,0,0)).x)) ||
		     ((camera.ViewportToWorldPoint(new Vector3(1,0,0)).x >= rightEdge.position.x) && (average > camera.ViewportToWorldPoint(new Vector3((float)0.5,0,0)).x)) ) {
			newPos = new Vector3( (float) transform.position.x, (float) transform.position.y, (float) transform.position.z);
//			if (Time.time % 3 < 0.01) Debug.Log ("Using current for xPos");
		} else {
			newPos = new Vector3( average, (float) transform.position.y, (float) transform.position.z);
//			if (Time.time % 3 < 0.01) Debug.Log ("Using average for xPos");
		}
		transform.position = newPos;
		playerCenter = newPos;
//		if (Time.time % 3 < 1) Debug.Log ("Camera X should be: " + newPos + " and is, in fact: " + transform.position.x);
	}	
}
