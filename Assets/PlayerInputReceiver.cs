using UnityEngine;
using System.Collections;

public class PlayerInputReceiver : MonoBehaviour {
	
	public Damager damager;

	public float attackDuration = 0.2f;
	private float attackDurationTimer = 0f;
	public float attackCooldown = 0.2f;
	private float attackCooldownTimer = 0f;
	private bool attacking = false;
	private bool attackReady = true;

	// Attack class, for specifying properties of various
	// attacks
	public class Attack {

		// Basic properties //
		public string name;
		public Vector2 origin;
		public float rotation;

		// Attack shape properties //
		// height and width multipliers for the shape
		public Vector2 boundsMultipliers;

		// Attack damage properties //
		// damage amount
		public float damage;
		// knockback
		public float knockback;

		public Attack(string name, Vector2 origin,
			             float rotation, Vector2 boundsMultipliers,
		              float damage, float knockback) {
			this.name = name;
			this.origin = origin;
			this.rotation = rotation;
			this.boundsMultipliers = boundsMultipliers;
			this.damage = damage;
			this.knockback = knockback;
		}

	}

	public Attack ForwardAttack;
	public Attack UpAttack;
	public Attack DownAttack;

	// control vars
	public bool facingLeft = false;

	// Use this for initialization
	void Start () {

		// Initialize attacks
		ForwardAttack = new Attack (
			"ForwardAttack",
			new Vector2 (1.850472f, 0.2485764f),
			0,
			new Vector2 (2, 3),
			10,
			20
			);
		UpAttack = new Attack (
			"UpAttack",
			new Vector2 (0, 2.428983f),
			90, // rotate 90 degrees so it faces upwards
			new Vector2 (2, 3),
			10,
			20
			);
		DownAttack = new Attack (
			"DownAttack",
			new Vector2 (0, -1.728983f),
			-90, // rotate 90 degrees down so it goes down
			new Vector2 (2, 3),
			10,
			20
			);
	
	}
	
	// Update is called once per frame
	void Update() {

		if (Input.GetKeyDown("left") || Input.GetAxis("Horizontal") < -0.5) {
			facingLeft = true;
		}
		else if (Input.GetKeyDown ("right") || Input.GetAxis ("Horizontal") > 0.5) {
			facingLeft = false;
		}

		#region Attacking
		// Use the attack button to active the damager
		// GetButtonDown only fires once, when the button is pressed, not continuously
		if (Input.GetButtonDown ("Attack") && !attacking && attackReady) {

			if (Input.GetAxis("Vertical") > 0.5) {
				damager.transform.localPosition = UpAttack.origin;
				damager.transform.localRotation = Quaternion.AngleAxis(UpAttack.rotation, Vector3.forward);
				damager.transform.localScale = new Vector3(UpAttack.boundsMultipliers.x, UpAttack.boundsMultipliers.y, 1);
			}

			else if (Input.GetAxis("Vertical") < -0.5) {
				damager.transform.localPosition = DownAttack.origin;
				damager.transform.localRotation = Quaternion.AngleAxis(DownAttack.rotation, Vector3.forward);
				damager.transform.localScale = new Vector3(DownAttack.boundsMultipliers.x, DownAttack.boundsMultipliers.y, 1);
			}

			else if (facingLeft) {
				damager.transform.localPosition =
					new Vector2(-ForwardAttack.origin.x, ForwardAttack.origin.y); // flip x
				damager.transform.localRotation = Quaternion.AngleAxis(ForwardAttack.rotation, Vector3.forward);
				damager.transform.localScale = new Vector3(ForwardAttack.boundsMultipliers.x, ForwardAttack.boundsMultipliers.y, 1);
			}

			else {
				damager.transform.localPosition = ForwardAttack.origin;
				damager.transform.localRotation = Quaternion.AngleAxis(ForwardAttack.rotation, Vector3.forward);
				damager.transform.localScale = new Vector3(ForwardAttack.boundsMultipliers.x, ForwardAttack.boundsMultipliers.y, 1);
			}

			attacking = true; // start the attack
			attackReady = false; // because we just attacked
		}

		if (attackReady == false) {
			if (attackCooldownTimer >= attackCooldown) {
				attackCooldownTimer = 0f;
				attackReady = true;
			}
			else {
				attackCooldownTimer += Time.deltaTime;
			}
		}

		if (attacking) {
			damager.gameObject.SetActive(true);
			damager.Damage();

			if (attackDurationTimer >= attackDuration) {
				attackDurationTimer = 0f;
				attacking = false;
			}
			else {
				attackDurationTimer += Time.deltaTime;
			}
			// Note: currently this will re-damage every frame
			// need to make sure enemies have temp invulnerable frames
		}
		else {
			damager.gameObject.SetActive(false);
		}
		#endregion
	
	}
}
