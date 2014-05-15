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
	Vector3 barPosition;
	public float barTextureLength = 793;
	public float barTextureHeight = 289;
	float barScaleFactor;

	float healthFillXScale = 0.7f;
	float healthFillYScale = 0.125f;
	float specialFillXScale = 0.6f;

	int xOffset;
	int yOffsetHealth;
	int yOffsetSpecial;

	// Use this for initialization
	void Awake () {   
		// First, set barScaleFactor such that the height of the health bars is the upper 20% of the screen.
		barScaleFactor = (0.2f * Screen.height) / barTextureHeight;

		// 793 x 289 is the scale for the health bar png.
		barTextureLength *= barScaleFactor; 
		barTextureHeight *= barScaleFactor;

		xOffset = (int) (0.3f * barTextureLength);
		yOffsetHealth = (int) (0.35f * barTextureHeight);
		yOffsetSpecial = (int) (0.5f * barTextureHeight);

		// Initialize character attributes
		charCombat = character.GetComponent<CombatScript> ();
		charMover = character.GetComponent<MovementAnimationScript> ();
		startHealth = charCombat.startHealth;
		charOnLeft = !charMover.facingLeft;

		// Initialize special attack strength.
		maxSpecialStrength = charCombat.maxSpecialStrength;
		
		// The health bar will be flipped and shifted from the left side of the screen
		// if the character's position is on the right.
		if (!charOnLeft){
			float spaceFromLeft = (float) (1f - (1.02 * (barTextureLength/Screen.width)));
			barPosition = Camera.main.ViewportToScreenPoint( new Vector3( spaceFromLeft, 0.01f, 0f) );

		} else { 
			barPosition = Camera.main.ViewportToScreenPoint( new Vector3(0f, 0.01f, 0f) ); 
		}
//		Debug.Log (gameObject + "'s barPosition is: " + barPosition);
//		Debug.Log ("The bar length and height for " + gameObject + " are: " + barTextureLength + " and " + barTextureHeight);
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnGUI () {
		// Draw the red health bar.
		GUI.Box (new Rect (barPosition.x + xOffset, 
		                   barPosition.y + yOffsetHealth, 
		                   ((float) currentHealth / startHealth) * barTextureLength * healthFillXScale, 
		                   barTextureHeight * healthFillYScale), 
		         healthBar, 
		         healthBarStyle);

		// Draw the yellow special bar.
		GUI.Box (new Rect (barPosition.x + xOffset, 
		                   barPosition.y + yOffsetSpecial, 
		                   ((float) currentSpecialStrength / maxSpecialStrength) * barTextureLength * specialFillXScale, 
		                   barTextureHeight * healthFillYScale), 
		         specialBar, 
		         specialBarStyle);
		// Draw the bar frame. 
		GUI.DrawTexture (new Rect (barPosition.x, barPosition.y, barTextureLength, barTextureHeight),
		                 barFrame);

	}
	

}