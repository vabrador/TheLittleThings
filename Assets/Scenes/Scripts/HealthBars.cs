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

	public float healthBarPositionX;
	public float healthBarPositionY;

	public Texture2D barFrame; 
	public Texture2D healthBar; 
	public Texture2D specialBar;

	public GUIStyle barStyle;
	
	public float healthBarLength;
	
	// Use this for initialization
	void Start () {   
		healthBarLength = Screen.width / 5;   
		startHealth = character.GetComponent<CombatScript>().startHealth;
		currentHealth = character.GetComponent<CombatScript>().currentHealth;
		healthBarPositionX = Screen.width / 2 + displacementFromCenter;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI () {
		// Create one Group to contain both images
		// Adjust the first 2 coordinates to place it somewhere else on-screen
		GUI.BeginGroup (new Rect (healthBarPositionX, healthBarPositionY, healthBarLength, 32));
		
		// Draw the background image
		GUI.Box (new Rect (healthBarPositionX, healthBarPositionY, healthBarLength, 32), barFrame, barStyle);
		
		// Create a second Group which will be clipped
		// We want to clip the image and not scale it, which is why we need the second Group
		GUI.BeginGroup (new Rect (healthBarPositionX, healthBarPositionY, currentHealth * healthBarLength / startHealth, 32));
		
		// Draw the foreground image
		GUI.Box (new Rect (healthBarPositionX, healthBarPositionY, healthBarLength, 32), healthBar);
		
		// End both Groups
		GUI.EndGroup ();
		
		GUI.EndGroup ();
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