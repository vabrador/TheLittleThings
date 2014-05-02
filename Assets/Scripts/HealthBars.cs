using UnityEngine;
using System.Collections;

public class HealthBars : MonoBehaviour {

	public int startHealth = CombatScript.startHealth;
	public int currentHealth = CombatScript.currentHealth;
	
	public Texture2D bgImage; 
	public Texture2D fgImage; 
	
	public float healthBarLength;
	
	// Use this for initialization
	void Start () {   
		healthBarLength = Screen.width /2;    
	}
	
	// Update is called once per frame
	void Update () {
		AdjustCurrentHealth(0);
	}
	
	void OnGUI () {
		// Create one Group to contain both images
		// Adjust the first 2 coordinates to place it somewhere else on-screen
		GUI.BeginGroup (new Rect (0,0, healthBarLength,32));
		
		// Draw the background image
		GUI.Box (new Rect (0,0, healthBarLength,32), bgImage);
		
		// Create a second Group which will be clipped
		// We want to clip the image and not scale it, which is why we need the second Group
		GUI.BeginGroup (new Rect (0,0, currentHealth / startHealth * healthBarLength, 32));
		
		// Draw the foreground image
		GUI.Box (new Rect (0,0,healthBarLength,32), fgImage);
		
		// End both Groups
		GUI.EndGroup ();
		
		GUI.EndGroup ();
	}
	
	public void AdjustCurrentHealth(int adj){
		
		currentHealth += adj;
		
		if(currentHealth < 0)
			currentHealth = 0;
		
		if(currentHealth > startHealth)
			currentHealth = startHealth;
		
		if(startHealth < 1)
			startHealth = 1;
		
		healthBarLength =(Screen.width /2) * (currentHealth / (float)startHealth);
	}
}