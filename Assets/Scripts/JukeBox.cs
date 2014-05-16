using UnityEngine;
using System.Collections;

public class JukeBox : MonoBehaviour {
	public AudioSource musicPlayerAudioSource;
	public AudioClip mainTheme;
	public float musicVolume = 0.5f;

	// Use this for initialization
	void Start () {
		musicPlayerAudioSource.volume = musicVolume;
		musicPlayerAudioSource.clip = mainTheme;
        musicPlayerAudioSource.loop = true;
        musicPlayerAudioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
