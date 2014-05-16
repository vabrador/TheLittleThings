using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboManager : MonoBehaviour {
	public GameObject playerChar;
	MovementAnimationScript charMover;
//	CombatScript charCombat;

	public float combatWindowDuration = 2f;
	int currentComboCount;
	float mostRecentHitTime;


	Dictionary<int, Texture2D> resourceMap;
	bool spriteRendering = false;

	
	Vector3 spritePos {
		get{
			float xPos;
			if (charMover.facingLeft) {xPos = 0.7f; }
			else {xPos = 0.2f; }
			Vector3 spritePoint = Camera.main.ViewportToScreenPoint(new Vector3(xPos, 0.225f, 0));
			return spritePoint;
		}
	}

	// Use this for initialization
	void Start () {
		if (playerChar == null) { playerChar = GameObject.Find ("Linda Left"); }
		charMover = playerChar.GetComponent<MovementAnimationScript> ();
		resourceMap = new Dictionary<int, Texture2D>() {
			{2, (Texture2D) Resources.Load ("combo2")},
			{3, (Texture2D) Resources.Load ("combo3")},
			{4, (Texture2D) Resources.Load ("combo4")}
		};
	}
	
	// Update is called once per frame
	void Update () {
		comboTimeout ();
	}

	public float spriteWidth = 0.15f;
	void OnGUI() {
		if (spriteRendering) {
			Texture2D comboTexture = resourceMap[currentComboCount];
			float scaleFactor = (Screen.width * spriteWidth) / (float) comboTexture.width;
//			Debug.Log (playerChar + " 's current scaleFactor is " + scaleFactor);
//			Debug.Log ("Based on: " + Screen.width + "*" + spriteWidth + "/" + comboTexture.width);
			GUI.DrawTexture(new Rect(spritePos.x, spritePos.y, (comboTexture.width * scaleFactor), (comboTexture.height * scaleFactor)) , comboTexture);
		}
	}

	public void hitLanded() {
		Debug.Log (playerChar + " just landed a hit!");
		mostRecentHitTime = Time.time;
		if (currentComboCount < 4) currentComboCount += 1;
		if (currentComboCount > 1) spriteRendering = true;
	}

	public void hitTaken() {
		Debug.Log (playerChar + " just took a hit!");
		currentComboCount = 0;
		spriteRendering = false;
	}

	void comboTimeout() {
		if ( (Time.time - mostRecentHitTime > combatWindowDuration) && (spriteRendering) ) {
			currentComboCount = 0;
			spriteRendering = false;
		}
	}

}
