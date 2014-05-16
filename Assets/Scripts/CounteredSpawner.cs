using UnityEngine;
using System.Collections;

public class CounteredSpawner : MonoBehaviour {
	public float spriteScreenDuration = 0.75f;
	public float counterGraphicScale = 0.3f;
	public float counterGraphicHeight = 0.65f;
	float counterTime = -1f; // Why does counter time start negative?  It keeps the graphic from displaying at the beginning when Time.time == 0!
	Vector3 playerCenterPoint;
	CameraFollower camFollower;
	Vector3 spriteSpawnPoint {
		get { return new Vector3(playerCenterPoint.x - graphicWidth/2, playerCenterPoint.y - graphicHeight*2, 0); }
	}
	Texture2D counterGraphic;


	// These variables store the height and width of the graphic.
	float graphicHeight;
	float graphicWidth;

	// Use this for initialization
	void Start () {
		camFollower = GameObject.Find ("Main Camera").GetComponent<CameraFollower> ();
		playerCenterPoint = Camera.main.WorldToScreenPoint(camFollower.playerCenter);
		counterGraphic = (Texture2D) Resources.Load ("counterGraphic");
		float scaleFactor = (Screen.width * counterGraphicScale) / (float)counterGraphic.width;
		graphicHeight = counterGraphic.height * scaleFactor;
		graphicWidth = counterGraphic.width * scaleFactor;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void registerCounter() {
		counterTime = Time.time;
	}

	void OnGUI() {
		if (Time.time - counterTime < spriteScreenDuration){
			Debug.Log ("Sprite should be drawing at " + spriteSpawnPoint);
			GUI.DrawTexture(new Rect(spriteSpawnPoint.x, spriteSpawnPoint.y, graphicWidth, graphicHeight) , counterGraphic);
		}

	}
}
