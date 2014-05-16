using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VocalFighter : MonoBehaviour {

    ///////////////////////
    // Utility Variables //
    ///////////////////////

    public AudioSource fighterAudioSource;
    public AudioSource fighterSFXSource;

    public float speechProbability = 0.5f;

    ////////////////////
    // Attempt Sounds //
    ////////////////////
    /* Attempt sounds are played as soon as
     * a given action is attempted, whether or
     * not it is successful.
     */

    public AudioClip attackAttemptSound1;
    public AudioClip attackAttemptSound2;
    public AudioClip attackAttemptSound3;
    public AudioClip attackAttemptSound4;
    public AudioClip attackAttemptSound5;
    private ArrayList attackAttemptSounds = new ArrayList(); // Initialized in Start.

    public AudioClip dashAttemptSound1;
    public AudioClip dashAttemptSound2;
    public AudioClip dashAttemptSound3;
    public AudioClip dashAttemptSound4;
    public AudioClip dashAttemptSound5;
    private ArrayList dashAttemptSounds = new ArrayList();
    public AudioClip dashAttemptFX;

    public AudioClip blockAttemptSound1;
    public AudioClip blockAttemptSound2;
    public AudioClip blockAttemptSound3;
    public AudioClip blockAttemptSound4;
    public AudioClip blockAttemptSound5;
    private ArrayList blockAttemptSounds = new ArrayList();
    public AudioClip blockAttemptFX;

    public AudioClip specialSound1;
    public AudioClip specialSound2;
    public AudioClip specialSound3;
    public AudioClip specialSound4;
    public AudioClip specialSound5;
    private ArrayList specialSounds = new ArrayList();
    public AudioClip specialFX;

    ////////////////////
    // Success Sounds //
    ////////////////////
    /* Success sounds are played upon the
     * successful completion of an action.
     */ 

    public AudioClip attackSuccessSound1;
    public AudioClip attackSuccessSound2;
    public AudioClip attackSuccessSound3;
    public AudioClip attackSuccessSound4;
    public AudioClip attackSuccessSound5;
    private ArrayList attackSuccessSounds = new ArrayList();
    public AudioClip attackSuccessFX;

    public AudioClip dashSuccessSound1;
    public AudioClip dashSuccessSound2;
    public AudioClip dashSuccessSound3;
    public AudioClip dashSuccessSound4;
    public AudioClip dashSuccessSound5;
    private ArrayList dashSuccessSounds = new ArrayList();

    public AudioClip blockSuccessSound1;
    public AudioClip blockSuccessSound2;
    public AudioClip blockSuccessSound3;
    public AudioClip blockSuccessSound4;
    public AudioClip blockSuccessSound5;
    private ArrayList blockSuccessSounds = new ArrayList();

    public AudioClip counterSuccessSound1;
    public AudioClip counterSuccessSound2;
    public AudioClip counterSuccessSound3;
    public AudioClip counterSuccessSound4;
    public AudioClip counterSuccessSound5;
    private ArrayList counterSuccessSounds = new ArrayList();

    //////////////////////
    // Defensive sounds //
    //////////////////////
    /* Defensive sounds are played in response to
     * a hit or some other action performed on the
     * fighter.
     */

    public AudioClip hitByAttackSound1;
    public AudioClip hitByAttackSound2;
    public AudioClip hitByAttackSound3;
    public AudioClip hitByAttackSound4;
    public AudioClip hitByAttackSound5;
    private ArrayList hitByAttackSounds = new ArrayList();

    public AudioClip counteredSound1;
    public AudioClip counteredSound2;
    public AudioClip counteredSound3;
    public AudioClip counteredSound4;
    public AudioClip counteredSound5;
    private ArrayList counteredSounds = new ArrayList();

    public AudioClip blockBrokenSound1;
    public AudioClip blockBrokenSound2;
    public AudioClip blockBrokenSound3;
    public AudioClip blockBrokenSound4;
    public AudioClip blockBrokenSound5;
    private ArrayList blockBrokenSounds = new ArrayList();
    public AudioClip blockBrokenFX;

    //////////////////////////////
    // Initialization Functions //
    //////////////////////////////

    // Called as soon as this script is loaded.
	void Start() {
		// Make a list of all the possible sounds for each sound type.
		AudioClip[] attackAttemptSoundPool = new AudioClip[]{attackAttemptSound1, attackAttemptSound2, attackAttemptSound3,	attackAttemptSound4, attackAttemptSound5};
		AudioClip[] dashAttemptSoundPool = new AudioClip[]{dashAttemptSound1, dashAttemptSound2, dashAttemptSound3,	dashAttemptSound4, dashAttemptSound5};
		AudioClip[] blockAttemptSoundPool = new AudioClip[]{blockAttemptSound1, blockAttemptSound2, blockAttemptSound3,	blockAttemptSound4, blockAttemptSound5};
		AudioClip[] specialSoundPool = new AudioClip[]{specialSound1, specialSound2, specialSound3, specialSound4, specialSound5};
		AudioClip[] attackSuccessSoundPool = new AudioClip[]{attackSuccessSound1, attackSuccessSound2, attackSuccessSound3,	attackSuccessSound4, attackSuccessSound5};
		AudioClip[] dashSuccessSoundPool = new AudioClip[]{dashSuccessSound1, dashSuccessSound2, dashSuccessSound3,	dashSuccessSound4, dashSuccessSound5};
		AudioClip[] blockSuccessSoundPool = new AudioClip[]{blockSuccessSound1, blockSuccessSound2, blockSuccessSound3,	blockSuccessSound4, blockSuccessSound5};
		AudioClip[] counterSuccessSoundPool = new AudioClip[]{counterSuccessSound1, counterSuccessSound2, counterSuccessSound3,	counterSuccessSound4, counterSuccessSound5};
		AudioClip[] hitByAttackSoundPool = new AudioClip[]{hitByAttackSound1, hitByAttackSound2, hitByAttackSound3,	hitByAttackSound4, hitByAttackSound5};
		AudioClip[] counteredSoundPool = new AudioClip[]{counteredSound1, counteredSound2, counteredSound3, counteredSound4, counteredSound5};
		AudioClip[] blockBrokenSoundPool = new AudioClip[]{blockBrokenSound1, blockBrokenSound2, blockBrokenSound3,	blockBrokenSound4, blockBrokenSound5};
		
		// Form a dictionary that matches up the pool of possible sounds with the list they're supposed to be added to.
		Dictionary<AudioClip[], ArrayList> poolToListMap = new Dictionary<AudioClip[], ArrayList>(){
			{attackAttemptSoundPool, attackAttemptSounds},
			{blockAttemptSoundPool, blockAttemptSounds},
			{dashAttemptSoundPool, dashAttemptSounds},
			{specialSoundPool, specialSounds},
			{attackSuccessSoundPool, attackSuccessSounds},
			{dashSuccessSoundPool, dashSuccessSounds},
			{blockSuccessSoundPool, blockSuccessSounds},
			{counterSuccessSoundPool, counterSuccessSounds},
			{hitByAttackSoundPool, hitByAttackSounds},
			{counteredSoundPool, counteredSounds},
			{blockBrokenSoundPool, blockBrokenSounds}
		};
		
		
		// Check if each sound is null and add it to the appropriate sound list if it isn't.
		ArrayList pools = new ArrayList(poolToListMap.Keys);
		foreach (AudioClip[] pool in pools){
			foreach(AudioClip clip in pool){
				if (clip != null) {
					poolToListMap[pool].Add (clip);
				}
			}
		}
		
	}

    /////////////////////////////
    // Sound-playing Functions //
    /////////////////////////////
    /* These functions won't necessarily guarantee that
     * a sound will be played. In particular, this script
     * only plays a sound if the FighterAudioSource is
     * not already playing a sound.
     */

    public void OnAttackAttempt() {

        if (!fighterAudioSource.isPlaying) {

            if (Random.Range(0f, 1f) < speechProbability && attackAttemptSounds.Count > 0) {

                fighterAudioSource.clip = (AudioClip)attackAttemptSounds[Random.Range(0, attackAttemptSounds.Count)];
                fighterAudioSource.Play();

            }

        }

    }
    public void OnDashAttempt() {

        if (!fighterAudioSource.isPlaying) {

            if (Random.Range(0f, 1f) < speechProbability && dashAttemptSounds.Count > 0) {

                fighterAudioSource.clip = (AudioClip)dashAttemptSounds[Random.Range(0, dashAttemptSounds.Count)];
                fighterAudioSource.Play();

            }

        }

        // SFX
        fighterSFXSource.PlayOneShot(dashAttemptFX);

    }
    public void OnBlockAttempt() {

        if (!fighterAudioSource.isPlaying) {

            if (Random.Range(0f, 1f) < speechProbability && blockAttemptSounds.Count > 0) {

                fighterAudioSource.clip = (AudioClip)blockAttemptSounds[Random.Range(0, blockAttemptSounds.Count)];
                fighterAudioSource.Play();

            }

        }

        // SFX
        if (blockAttemptFX != null)
            fighterSFXSource.PlayOneShot(blockAttemptFX);

    }
    public void OnSpecial() {

        if (!fighterAudioSource.isPlaying) {

            if (specialSounds.Count > 0) {

                // Specials ALWAYS have an associated sound (ignore speech probability)
                fighterAudioSource.clip = (AudioClip)specialSounds[Random.Range(0, specialSounds.Count)];
                fighterAudioSource.Play();

            }

        }

        // SFX
        fighterSFXSource.PlayOneShot(specialFX);

    }
    public void OnAttackSuccess() {

        if (!fighterAudioSource.isPlaying) {

            if (Random.Range(0f, 1f) < speechProbability && attackSuccessSounds.Count > 0) {

                fighterAudioSource.clip = (AudioClip)attackSuccessSounds[Random.Range(0, attackSuccessSounds.Count)];
                fighterAudioSource.Play();

            }

        }

        // SFX
        fighterSFXSource.PlayOneShot(attackSuccessFX);

    }
    public void OnDashSuccess() {

        if (!fighterAudioSource.isPlaying) {

            if (Random.Range(0f, 1f) < speechProbability && dashSuccessSounds.Count > 0) {

                fighterAudioSource.clip = (AudioClip)dashSuccessSounds[Random.Range(0, dashSuccessSounds.Count)];
                fighterAudioSource.Play();

            }

        }

    }
    public void OnBlockSuccess() {

        if (!fighterAudioSource.isPlaying) {

            if (Random.Range(0f, 1f) < speechProbability && blockSuccessSounds.Count > 0) {

                fighterAudioSource.clip = (AudioClip)blockSuccessSounds[Random.Range(0, blockSuccessSounds.Count)];
                fighterAudioSource.Play();

            }

        }

    }
    public void OnCounterSuccess() {

        if (!fighterAudioSource.isPlaying) {

            if (Random.Range(0f, 1f) < speechProbability && counterSuccessSounds.Count > 0) {

                fighterAudioSource.clip = (AudioClip)counterSuccessSounds[Random.Range(0, counterSuccessSounds.Count)];
                fighterAudioSource.Play();

            }

        }

    }
    public void OnHitByAttack() {

        if (!fighterAudioSource.isPlaying) {

            if (Random.Range(0f, 1f) < speechProbability && hitByAttackSounds.Count > 0) {

                fighterAudioSource.clip = (AudioClip)hitByAttackSounds[Random.Range(0, hitByAttackSounds.Count)];
                fighterAudioSource.Play();

            }

        }

    }
    public void OnCountered() {

        if (!fighterAudioSource.isPlaying) {

            if (Random.Range(0f, 1f) < speechProbability && counteredSounds.Count > 0) {

                fighterAudioSource.clip = (AudioClip)counteredSounds[Random.Range(0, counteredSounds.Count)];
                fighterAudioSource.Play();

            }

        }

    }
    public void OnBlockBroken() {

        if (!fighterAudioSource.isPlaying) {

            if (Random.Range(0f, 1f) < speechProbability && blockBrokenSounds.Count > 0) {

                fighterAudioSource.clip = (AudioClip)blockBrokenSounds[Random.Range(0, blockBrokenSounds.Count)];
                fighterAudioSource.Play();

            }

        }

        // SFX
        fighterSFXSource.PlayOneShot(blockAttemptFX);

    }

    ///////////////////////
    // Utility Functions //
    ///////////////////////


}
