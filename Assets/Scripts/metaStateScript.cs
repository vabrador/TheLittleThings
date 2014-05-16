using UnityEngine;
using System.Collections;

public class metaStateScript : MonoBehaviour {
	public GameObject leftChar;
	public GameObject rightChar;

	PlayerControl leftControl;
	PlayerControl rightControl;

	public AudioSource announcerAudioSource;
	public float announcerVolume = 1f;
	public AudioClip startSound;
	public AudioClip winSound;
	public AudioClip loseSound;


	// Use this for initialization
	void Start () {
		leftControl = leftChar.GetComponent<PlayerControl>();
		rightControl = rightChar.GetComponent<PlayerControl>();
	}

    void Awak() {
        announcerAudioSource.clip = startSound;
        announcerAudioSource.volume = announcerVolume;
        announcerAudioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void endGame(bool win) {
		leftControl.gameRunning = false;
		rightControl.gameRunning = false;
		Vector3 currentCenter = Camera.main.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0f));
		currentCenter.z = leftChar.transform.position.z;
		if (win) {
			Instantiate(Resources.Load ("lindaWinGraphic"), currentCenter, Quaternion.identity);
			announcerAudioSource.clip = winSound;
		} else {
			Instantiate(Resources.Load ("georgeWinGraphic"), currentCenter, Quaternion.identity);
			announcerAudioSource.clip = loseSound;
		}
		announcerAudioSource.Play();
	}
}
