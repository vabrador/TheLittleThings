using UnityEngine;
using System.Collections;

public class VocalFighter : MonoBehaviour {

    ///////////////////////
    // Utility Variables //
    ///////////////////////

    public AudioSource fighterAudioSource;

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

    public AudioClip blockAttemptSound1;
    public AudioClip blockAttemptSound2;
    public AudioClip blockAttemptSound3;
    public AudioClip blockAttemptSound4;
    public AudioClip blockAttemptSound5;
    private ArrayList blockAttemptSounds = new ArrayList();

    public AudioClip specialSound1;
    public AudioClip specialSound2;
    public AudioClip specialSound3;
    public AudioClip specialSound4;
    public AudioClip specialSound5;
    private ArrayList specialSounds = new ArrayList();

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

    //////////////////////////////
    // Initialization Functions //
    //////////////////////////////

    // Called as soon as this script is loaded.
    void Start() {

        /* For each type of sound, we will check which ones are null,
         * and initialize the associated ArrayList that will contain
         * each of a particular kind of sound.
         */

        #region Sound Array Initialization

        // attackAttempt sounds
        if (attackAttemptSound1 != null) {
            attackAttemptSounds.Add(attackAttemptSound1);
        }
        if (attackAttemptSound2 != null) {
            attackAttemptSounds.Add(attackAttemptSound2);
        }
        if (attackAttemptSound3 != null) {
            attackAttemptSounds.Add(attackAttemptSound3);
        }
        if (attackAttemptSound4 != null) {
            attackAttemptSounds.Add(attackAttemptSound4);
        }
        if (attackAttemptSound5 != null) {
            attackAttemptSounds.Add(attackAttemptSound5);
        }

        // dashAttempt sounds
        if (dashAttemptSound1 != null) {
            dashAttemptSounds.Add(dashAttemptSound1);
        }
        if (dashAttemptSound2 != null) {
            dashAttemptSounds.Add(dashAttemptSound2);
        }
        if (dashAttemptSound3 != null) {
            dashAttemptSounds.Add(dashAttemptSound3);
        }
        if (dashAttemptSound4 != null) {
            dashAttemptSounds.Add(dashAttemptSound4);
        }
        if (dashAttemptSound5 != null) {
            dashAttemptSounds.Add(dashAttemptSound5);
        }

        // blockAttempt sounds
        if (blockAttemptSound1 != null) {
            blockAttemptSounds.Add(blockAttemptSound1);
        }
        if (blockAttemptSound2 != null) {
            blockAttemptSounds.Add(blockAttemptSound2);
        }
        if (blockAttemptSound3 != null) {
            blockAttemptSounds.Add(blockAttemptSound3);
        }
        if (blockAttemptSound4 != null) {
            blockAttemptSounds.Add(blockAttemptSound4);
        }
        if (blockAttemptSound5 != null) {
            blockAttemptSounds.Add(blockAttemptSound5);
        }

        // Special move sounds
        if (specialSound1 != null) {
            specialSounds.Add(specialSound1);
        }
        if (specialSound2 != null) {
            specialSounds.Add(specialSound2);
        }
        if (specialSound3 != null) {
            specialSounds.Add(specialSound3);
        }
        if (specialSound4 != null) {
            specialSounds.Add(specialSound4);
        }
        if (specialSound5 != null) {
            specialSounds.Add(specialSound5);
        }

        // attackSuccess sounds
        if (attackSuccessSound1 != null) {
            attackSuccessSounds.Add(attackSuccessSound1);
        }
        if (attackSuccessSound2 != null) {
            attackSuccessSounds.Add(attackSuccessSound2);
        }
        if (attackSuccessSound3 != null) {
            attackSuccessSounds.Add(attackSuccessSound3);
        }
        if (attackSuccessSound4 != null) {
            attackSuccessSounds.Add(attackSuccessSound4);
        }
        if (attackSuccessSound5 != null) {
            attackSuccessSounds.Add(attackSuccessSound5);
        }

        // dashSuccess sounds
        if (dashSuccessSound1 != null) {
            dashSuccessSounds.Add(dashSuccessSound1);
        }
        if (dashSuccessSound2 != null) {
            dashSuccessSounds.Add(dashSuccessSound2);
        }
        if (dashSuccessSound3 != null) {
            dashSuccessSounds.Add(dashSuccessSound3);
        }
        if (dashSuccessSound4 != null) {
            dashSuccessSounds.Add(dashSuccessSound4);
        }
        if (dashSuccessSound5 != null) {
            dashSuccessSounds.Add(dashSuccessSound5);
        }

        // blockSuccess sounds
        if (blockSuccessSound1 != null) {
            blockSuccessSounds.Add(blockSuccessSound1);
        }
        if (blockSuccessSound2 != null) {
            blockSuccessSounds.Add(blockSuccessSound2);
        }
        if (blockSuccessSound3 != null) {
            blockSuccessSounds.Add(blockSuccessSound3);
        }
        if (blockSuccessSound4 != null) {
            blockSuccessSounds.Add(blockSuccessSound4);
        }
        if (blockSuccessSound5 != null) {
            blockSuccessSounds.Add(blockSuccessSound5);
        }

        // counterSuccess sounds
        if (counterSuccessSound1 != null) {
            counterSuccessSounds.Add(counterSuccessSound1);
        }
        if (counterSuccessSound2 != null) {
            counterSuccessSounds.Add(counterSuccessSound2);
        }
        if (counterSuccessSound3 != null) {
            counterSuccessSounds.Add(counterSuccessSound3);
        }
        if (counterSuccessSound4 != null) {
            counterSuccessSounds.Add(counterSuccessSound4);
        }
        if (counterSuccessSound5 != null) {
            counterSuccessSounds.Add(counterSuccessSound5);
        }

        // hitByAttack sounds
        if (hitByAttackSound1 != null) {
            hitByAttackSounds.Add(hitByAttackSound1);
        }
        if (hitByAttackSound2 != null) {
            hitByAttackSounds.Add(hitByAttackSound2);
        }
        if (hitByAttackSound3 != null) {
            hitByAttackSounds.Add(hitByAttackSound3);
        }
        if (hitByAttackSound4 != null) {
            hitByAttackSounds.Add(hitByAttackSound4);
        }
        if (hitByAttackSound5 != null) {
            hitByAttackSounds.Add(hitByAttackSound5);
        }

        // countered sounds
        if (counteredSound1 != null) {
            counteredSounds.Add(counteredSound1);
        }
        if (counteredSound2 != null) {
            counteredSounds.Add(counteredSound2);
        }
        if (counteredSound3 != null) {
            counteredSounds.Add(counteredSound3);
        }
        if (counteredSound4 != null) {
            counteredSounds.Add(counteredSound4);
        }
        if (counteredSound5 != null) {
            counteredSounds.Add(counteredSound5);
        }

        // blockBroken sounds
        if (blockBrokenSound1 != null) {
            blockBrokenSounds.Add(blockBrokenSound1);
        }
        if (blockBrokenSound2 != null) {
            blockBrokenSounds.Add(blockBrokenSound2);
        }
        if (blockBrokenSound3 != null) {
            blockBrokenSounds.Add(blockBrokenSound3);
        }
        if (blockBrokenSound4 != null) {
            blockBrokenSounds.Add(blockBrokenSound4);
        }
        if (blockBrokenSound5 != null) {
            blockBrokenSounds.Add(blockBrokenSound5);
        }

        #endregion

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

    }
    public void OnBlockAttempt() {

        if (!fighterAudioSource.isPlaying) {

            if (Random.Range(0f, 1f) < speechProbability && blockAttemptSounds.Count > 0) {

                fighterAudioSource.clip = (AudioClip)blockAttemptSounds[Random.Range(0, blockAttemptSounds.Count)];
                fighterAudioSource.Play();

            }

        }

    }
    public void OnSpecial() {

        if (!fighterAudioSource.isPlaying) {

            if (specialSounds.Count > 0) {

                // Specials ALWAYS have an associated sound (ignore speech probability)
                fighterAudioSource.clip = (AudioClip)attackSuccessSounds[Random.Range(0, attackSuccessSounds.Count)];
                fighterAudioSource.Play();

            }

        }

    }
    public void OnAttackSuccess() {

        if (!fighterAudioSource.isPlaying) {

            if (Random.Range(0f, 1f) < speechProbability && attackSuccessSounds.Count > 0) {

                fighterAudioSource.clip = (AudioClip)attackSuccessSounds[Random.Range(0, attackSuccessSounds.Count)];
                fighterAudioSource.Play();

            }

        }

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

    }

    ///////////////////////
    // Utility Functions //
    ///////////////////////


}
