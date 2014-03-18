using UnityEngine;
using System.Collections;

public class PlatformerMotor : MonoBehaviour {
	
	// Physical constants
	public Vector3 gravityForce = new Vector3(0f, -20f, 0f); // We'll multiply this by charBody.mass in initialization
	public float sideDragCoefficient;
	
	/// <summary>
	/// Drag (squared opposing force) is one of the best mechanisms to implement a good-feeling
	/// platformer controller in physical simulations. This drag in particular allows good
	/// acceleration for running or walking. Since the buttons will always correspond to movement
	/// perpendicular to the force of gravity (even if said gravity changes, for instance), we
	/// calculate the drag vector from a rotation of the gravity vector. A more optimized
	/// implementation would bake this vector in any situation where gravity won't be changing.
	/// </summary>
	Vector2 sideToSideMovementDragCoeff {
		get {
			return new Vector2(-gravityForce.normalized.y * sideDragCoefficient, gravityForce.normalized.y * sideDragCoefficient);
		}
	} // CURRENTLY UNUSED
	
	// Physical information
	public Vector3 position {
		get { return transform.position; }
	}
	public Vector3 velocity {
		get { return charBody.velocity; }
		set {
			charBody.velocity = value;
		}
	}
	
	// Control / movement constants
	public float moveForce;
	public float jumpImpulse; // instant acceleration
	public float shortHopSpeed; // max vertical speed when not pressing the jump button, for short-hopping
	public float idleMovementFriction;

	public bool grounded;

	public bool Grounded {
		get {
			return IsGrounded();
		}
	}
	
	// Character Controller variables
	public Rigidbody2D charBody;
	public CollisionDetector footCircle;
	public CollisionDetector bodyRect;
	
	// Animation control variables
	public bool facingRight = false;
	public bool facingRightLastFrame = false;
	public bool jumping = false; // if we should play the jump animation
	public bool jumpToFalling = false; // used between jumps and falls to use
	// the transition animation
	public bool falling = false;
	
	// Use this for initialization
	void Start () {
		// We initially describe our forces in terms of acceleration.
		// Here, now that charBody is initialized, we multiply said accelerations by its mass to get the force we need to apply.
		gravityForce = gravityForce * charBody.mass;
		moveForce = moveForce * charBody.mass;
	}
	
	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;
		
		#region -- Physics Control Code --
		// whether or not we're touching the ground with the foot circle
		grounded = Grounded;

		// direction
		float direction = Input.GetAxis("Horizontal");
		
		// Friction if not pressing left or right and Grounded
		if (direction == 0 && Grounded) {
			footCircle.GetComponent<CircleCollider2D>().sharedMaterial.friction = idleMovementFriction;
		}
		else {
			footCircle.GetComponent<CircleCollider2D>().sharedMaterial.friction = 0;
		}

		// Side-to-side movement
		charBody.AddForce(new Vector3(direction * moveForce, 0f, 0f));
		charBody.AddForce(new Vector3((charBody.velocity.x > 0?-1:1)*(charBody.velocity.x * charBody.velocity.x) * sideDragCoefficient, 0f, 0f)); // side drag
		
		// Facing information
		if (direction > 0) {
			facingRight = true;
		}
		else if (direction < 0) {
			facingRight = false;
		}
		
		// Gravity
		//charBody.AddForce(gravityForce);
		
		// Jumping
		if (Input.GetButtonDown("Jump") && Grounded) {
			charBody.AddForce(new Vector2(0f, jumpImpulse / Time.deltaTime));
			jumping = true;
		}
		if (velocity.y > shortHopSpeed && !Input.GetButton("Jump")) {
			velocity = new Vector2 (velocity.x, shortHopSpeed); // TODO: This is not correct for arbitrary gravity directions
			// also it should add to the vector, not just reset it
			// Really I should implement this physically as an impulse vector and have it push against things
			// Newton's third law and all that
		}

		// Dashing
		//if (Input.GetButtonDown("Dash") && Grounded) {
		//	Debug.Log ("Dash!");
		//	charBody.velocity = new Vector2(charBody.velocity.x * 10, charBody.velocity.y);
		//}
		//Debug.Log (charBody.velocity.x);
		
		#endregion
		
		#region -- Animation Code --
		if (Grounded && Input.GetButtonDown("Jump")) {
			jumping = false;
			jumpToFalling = false;
			falling = false;
		}
		if (Mathf.Abs(velocity.x) > 0.5 && Grounded) {
			float ratio = Mathf.Abs(velocity.x) / velocity.x; // Not correct for arbitrary gravity
			//animationToPlay = "Edmund Run";
			//animationController.ClipFps = 0; // reset to default Clip FPS
			//animationController.ClipFps *= ratio;
		}
		else if (jumping && velocity.y > 0) {
			//animationToPlay = "Edmund Jump";
			//Debug.Log("We are not grounded and we're jumping and our velocity > 0 so we should jump");
		}
		else if (!Grounded && jumping && velocity.y <= 0) {
			//animationToPlay = "Edmund JumpToFall";
			falling = false;
			jumping = false;
			jumpToFalling = true;
			//Debug.Log("We are not grounded and we're jumping and our velocity < 0 so we are transitioning");
		}
		else if (!Grounded && jumpToFalling) {
			//int frameCount = animationController.GetClipByName("Edmund JumpToFall").frames.Length;
			//if (animationController.ClipTimeSeconds > (float)frameCount / animationController.ClipFps) { // This animation is done
				//animationToPlay = "Edmund Fall";
				//jumpToFalling = false;
				//falling = true;
				//Debug.Log("The jumpToFall animation is done so we should fall");
			//}
			//else animationToPlay = "Edmund JumpToFall";
		}
		else if (!Grounded) {
			//animationToPlay = "Edmund Fall";
			falling = true;
			//Debug.Log("We are not grounded but we're not jumping");
		}
		else {
			//Debug.Log("Okay just play standing " + grounded + "-grounded; " + jumping + "-jumping; " + falling + "-falling");
			//animationToPlay = "Edmund Stance";
		}
		
		// Reverse?
		if (facingRight != facingRightLastFrame) {
			//animationController.FlipX();
		}
		facingRightLastFrame = facingRight;
		
		// Play the animation
		//if (currentAnimation != animationToPlay) {
			//animationController.Play(animationToPlay);
			//currentAnimation = animationToPlay;
		//}
		#endregion
		
	}
	
	/// <summary>
	/// Whether or not the Actor is touching the ground.
	/// </summary>
	private bool IsGrounded() {
		//RaycastHit info;
		// This down here won't work for zero-gravity. Will need to change this to go down via the player's orientation in 3-space.
//		if (Physics.Raycast(new Ray(charBody.transform.position, gravityForce.normalized), out info, 100f)) {
//			Debug.Log("distance from ground is " + info.distance);
//			Debug.Log("charCollider height / 2 is " + (charCollider.size.y / 2f + 0.075f));
//			if (info.distance <= (charCollider.size.y / 2f) + 0.075f) // we're touching the ground
//				return true;
//		}
		return footCircle.IsColliding();
	}
}
