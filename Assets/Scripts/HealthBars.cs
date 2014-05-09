using UnityEngine;
using System.Collections;

public class HealthBars : MonoBehaviour {

	public GameObject character;

	public int startHealth;
	public int currentHealth;

	public int maxSpecialStrength;
	public int currentSpecialStrength;

	public float displacementFromCenter;

	// If the character is on the left, this should be 1. Else, it should be -1.
	public bool charOnLeft;

	public float healthBarPositionX;
	public float healthBarPositionY;
	
	public Texture2D barFrame; 
	public Texture2D healthBar; 
	public Texture2D specialBar;
	
	public GUIStyle healthBarStyle;
	public GUIStyle specialBarStyle;

	public float healthBarLength;
	public float healthBarHeight;
	public float scaleFactor;
	
	// Use this for initialization
	void Awake () {   
		// 793 x 289 is the scale for the health bar png.
		healthBarLength = 793 * scaleFactor; 
		healthBarHeight = 289 * scaleFactor;

		// Initialize health
		startHealth = character.GetComponent<CombatScript>().startHealth;
		currentHealth = startHealth;

		// Initialize special attack strength.
		maxSpecialStrength = character.GetComponent<CombatScript>().maxSpecialStrength;
		Debug.Log (currentSpecialStrength);
	}
	
	// Update is called once per frame
	void Update () {
		currentHealth = character.GetComponent<CombatScript>().currentHealth;
		currentSpecialStrength = character.GetComponent<CombatScript>().specialStrength;
	}
	
	void OnGUI () {
		
		// This vector object will fix the GUI's position relative to the camera.
		Vector3 healthBarPosition = Camera.main.ViewportToScreenPoint(new Vector3(healthBarPositionX, 
		                                                                          healthBarPositionY, 0));
		
		// Create one Group to contain all three images, the frame, the health bar, and the special bar.
		//GUI.BeginGroup (new Rect (healthBarPosition.x, healthBarPosition.y, healthBarLength, healthBarHeight));
		
		// The health bar will be flipped if the character's position is on the right.
		if (charOnLeft) {
			// Adjust the current health so that the bar isn't longer than 100% or less than 0%.
			AdjustCurrentHealth();

			// Draw the red health bar.
			// NOTE: The numbers here are only meant for the demo. They are hard coded numbers
			//       chosen so that the bars appear in the right spot on the screen. I can make
			//       work for a general case later.
			GUI.Box (new Rect (healthBarPosition.x + 113, 
			                   healthBarPosition.y + 45, 
			                   (currentHealth * healthBarLength / startHealth) * 0.70f, 
			                   healthBarHeight * 0.15f), 
			         healthBar, 
			         healthBarStyle);

			// Draw the yellow special bar.
			GUI.Box (new Rect (healthBarPosition.x + 113, 
			                   healthBarPosition.y + 65, 
			                   (currentSpecialStrength * healthBarLength / maxSpecialStrength) * 0.54f, 
			                   healthBarHeight * 0.15f), 
			         specialBar, 
			         specialBarStyle);
			// Draw the bar frame. 
			GUI.DrawTexture (new Rect (healthBarPosition.x, healthBarPosition.y, healthBarLength, healthBarHeight),
			                 barFrame);

			// Create a second Group which will be clipped
			// We want to clip the image and not scale it, which is why we need the second Group
			//GUI.BeginGroup (new Rect (healthBarLength * 0.25f, healthBarHeight * 0.25f, 
			//                          (currentHealth * healthBarLength / startHealth) * 0.75f, healthBarHeight * 0.2f));

		} else {

			// Adjust the current health so that the bar isn't longer than 100% or less than 0%.
			AdjustCurrentHealth();

			// Draw the red health bar.
			// NOTE: The numbers here are only meant for the demo. They are hard coded numbers
			//       chosen so that the bars appear in the right spot on the screen. I can make
			//       work for a general case later.
			GUI.Box (new Rect (healthBarPosition.x - 111, 
			                   healthBarPosition.y + 45, 
			                   -(currentHealth * healthBarLength / startHealth) * 0.70f, 
			                   healthBarHeight * 0.15f), 
			         healthBar, 
			         healthBarStyle);
			
			// Draw the yellow special bar.
			GUI.Box (new Rect (healthBarPosition.x - 111, 
			                   healthBarPosition.y + 65, 
			                   -(currentSpecialStrength * healthBarLength / maxSpecialStrength) * 0.54f, 
			                   healthBarHeight * 0.15f), 
			         specialBar, 
			         specialBarStyle);

			// Draw the health bar frame.
			GUI.DrawTexture (new Rect (healthBarPosition.x, healthBarPosition.y, -healthBarLength, healthBarHeight),
			                 barFrame);
		}

		// End both Groups
		//GUI.EndGroup ();
		//GUI.EndGroup ();
	}
	
	public void AdjustCurrentHealth() {
	
		if(currentHealth < 0)
			currentHealth = 0;
	
		if(currentHealth > startHealth)
			currentHealth = startHealth;
	}
}