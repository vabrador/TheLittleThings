using UnityEngine;
using System.Collections;

public class HealthBars : MonoBehaviour {

	public GameObject character;
	MovementAnimationScript charMover;
	CombatScript charCombat;

	// Character attribute values for health & special
	int startHealth;
	int currentHealth {
		get { return charCombat.currentHealth; }
	}
	int maxSpecialStrength;
	int currentSpecialStrength{
		get { return charCombat.specialStrength; }
	}

	// If the character is on the left, this should be 1. Else, it should be -1.
	bool charOnLeft;

	// Variables for Health, Special, and Combo textures/styles
	public Texture2D barFrame; 
	public Texture2D healthBar; 
	public Texture2D specialBar;
	public GUIStyle healthBarStyle;
	public GUIStyle specialBarStyle;

	// Constants for Health Bar position, size, scale
	public float healthBarPositionX;
	public float healthBarPositionY;
	public float defaultBarTextureLength = 793;
	public float defaultBarTextureHeight = 289;
	public float barScaleFactor;

	public float healthFillXScale = 0.7f;
	public float healthFillYScale = 0.15f;
	public float specialFillXScale = 0.54f;

	public int xOffset = 105;
	public int yOffsetHealth = 44;
	public int yOffsetSpecial = 60;
	
	// Use this for initialization
	void Awake () {   
		// 793 x 289 is the scale for the health bar png.
		defaultBarTextureLength *= barScaleFactor; 
		defaultBarTextureHeight *= barScaleFactor;

		// Initialize character attributes
		charCombat = character.GetComponent<CombatScript> ();
		charMover = character.GetComponent<MovementAnimationScript> ();
		startHealth = charCombat.startHealth;
		charOnLeft = !charMover.facingLeft;

		// Initialize special attack strength.
		maxSpecialStrength = charCombat.maxSpecialStrength;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnGUI () {
		
		// This vector object will fix the GUI's position relative to the camera.
		Vector3 healthBarPosition = Camera.main.ViewportToScreenPoint(new Vector3(healthBarPositionX, 
		                                                                          healthBarPositionY, 0));

		// The health bar will be flipped if the character's position is on the right.
		if (!charOnLeft){
			xOffset *= -1;
			defaultBarTextureLength *= -1;
		}

		// Draw the red health bar.
		// NOTE: The numbers here are only meant for the demo. They are hard coded numbers
		//       chosen so that the bars appear in the right spot on the screen. I can make
		//       work for a general case later.

		GUI.Box (new Rect (healthBarPosition.x + xOffset, 
		                   healthBarPosition.y + yOffsetHealth, 
		                   (currentHealth * defaultBarTextureLength / startHealth) * healthFillXScale, 
		                   defaultBarTextureHeight * healthFillYScale), 
		         healthBar, 
		         healthBarStyle);

		// Draw the yellow special bar.
		GUI.Box (new Rect (healthBarPosition.x + xOffset, 
		                   healthBarPosition.y + yOffsetSpecial, 
		                   (currentSpecialStrength * defaultBarTextureLength / maxSpecialStrength) * specialFillXScale, 
		                   defaultBarTextureHeight * healthFillYScale), 
		         specialBar, 
		         specialBarStyle);
		// Draw the bar frame. 
		GUI.DrawTexture (new Rect (healthBarPosition.x, healthBarPosition.y, defaultBarTextureLength, defaultBarTextureHeight),
		                 barFrame);

	}
	

}