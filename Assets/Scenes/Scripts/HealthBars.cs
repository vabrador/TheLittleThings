using UnityEngine;
using System.Collections;

public class HealthBars : MonoBehaviour {

	public GameObject character;
	public int startHealth;
	public int currentHealth;

	public float displacementFromCenter;

	public float healthBarPositionX;
	public float healthBarPositionY;
	
	public Texture2D barFrame; 
	public Texture2D healthBar; 
	public Texture2D specialBar;

	//public GUIStyle barStyle;
	
	public float healthBarLength;
	public float healthBarHeight;
	public float scaleFactor;
	
	// Use this for initialization
	void Start () {   
		// 793 x 289 is the scale for the health bar png.
		healthBarLength = 793 * scaleFactor; 
		healthBarHeight = 289 * scaleFactor;
		startHealth = character.GetComponent<CombatScript>().startHealth;
		currentHealth = character.GetComponent<CombatScript>().currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI () {

		// Create one Group to contain all three images, the frame, the health bar, and the special bar.
		// This vector object will fix the GUI's position relative to the camera.
		Vector3 healthBarPosition = Camera.main.ViewportToScreenPoint(new Vector3(healthBarPositionX, 
		                                                                          healthBarPositionY, 0));
		
		//GUI.BeginGroup (new Rect (healthBarPosition.x, healthBarPosition.y, healthBarLength, healthBarHeight));

		// Draw the background image
		GUI.DrawTexture (new Rect (healthBarPosition.x, healthBarPosition.y, healthBarLength, healthBarHeight), barFrame);
		
		// Create a second Group which will be clipped
		// We want to clip the image and not scale it, which is why we need the second Group
		//GUI.BeginGroup (new Rect (healthBarPosition.x, healthBarPosition.y, 
		//                          currentHealth * healthBarLength / startHealth, healthBarHeight));
		
		// Draw the foreground image
		//GUI.Box (new Rect (healthBarPosition.x, healthBarPosition.x, healthBarLength, healthBarHeight), healthBar);
		
		// End both Groups
		//GUI.EndGroup ();
		
		//GUI.EndGroup ();
	}
	
	//public void AdjustCurrentHealth() {
	
	//	if(CombatScript.currentHealth < 0)
	//		currentHealth = 0;
	
	//	if(currentHealth > startHealth)
	//		currentHealth = startHealth;
	
	//	if(startHealth < 1)
	//		startHealth = 1;
	
	//	healthBarLength =(Screen.width /2) * (currentHealth / (float)startHealth);
	//}
}