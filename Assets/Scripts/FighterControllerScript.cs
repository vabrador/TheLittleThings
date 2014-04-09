using UnityEngine;
using System.Collections;

public class FighterControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	Animator anim;
	bool facingRight;
	public string characterSide = "left";

	public Animation punchAnim;
	public Animation blockAnim;
	public Animation dashAnim;

	// Variables used to set how much time an opponent has
	// to counter a move after it begins 
	public float punchCounterZone = 1f;
	public float blockCounterZone = 1f;
	public float dashCounterZone = 1f;

	// Variables to keep track of when the most recent move
	// of each type started.
	float punchStart = 0f;
	float blockStart = 0f;
	float dashStart = 0f;

	// Variables to keep track of whether or not the character is
	// in a state of punching, blocking, or dashing
	bool punching = false;
	bool blocking = false;
	bool dashing = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		if (characterSide == "left")
						facingRight = true;
	}

	void Update() {
		CheckForMoves();
	}

	IEnumerator CheckForMoves() {
		if (Input.GetKeyDown (KeyCode.A)) {
			punchStart = Time.time;
			punching = true;
			yield return WaitForAnimation(punchAnim);
			punching = false;
		}
		else if (Input.GetKeyDown(KeyCode.S)) {
			blockStart = Time.time;
			blocking = true;
			yield return WaitForAnimation(blockAnim);
			blocking = false;
		}
		else if (Input.GetKeyDown (KeyCode.D)) {
			dashStart = Time.time;
			dashing = true;
			yield return WaitForAnimation(dashAnim);
			dashing = false;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		float move = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (move));
		rigidbody2D.velocity = new Vector2 (maxSpeed * move, rigidbody2D.velocity.y);

		if (move > 0 && !facingRight)
				Flip ();
		else if (move < 0 && facingRight)
				Flip ();
	}

	private IEnumerator WaitForAnimation(Animation animation)
	{
		do
		{
			yield return null;
		} while (animation.isPlaying);
	}

	void Flip() {
				facingRight = !facingRight;
				Vector3 localScale = transform.localScale;
				localScale.x *= -1;
				transform.localScale = localScale;
		}
}
