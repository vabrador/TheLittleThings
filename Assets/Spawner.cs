using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	// Spawn timer in seconds
	public float spawnTime = 1f;
	private float spawnTimer = 0f;
	public GameObject thingToSpawn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		spawnTimer += Time.deltaTime;

		if (spawnTimer >= spawnTime) {
			spawnTimer = 0f;
			Spawn();
		}
	
	}

	void Spawn() {
		Object.Instantiate (thingToSpawn, this.transform.position, this.transform.rotation);
	}
}
