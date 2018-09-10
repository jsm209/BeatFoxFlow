using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A Trap manages the appearence and colliders of a trap by checking the BeatManager to see if a
// beat has passed. If so, Trap will change accordingly. It can have pickup beats, active, and
// rest beats.


// Things to fix/I have questions:
// Is it really necessary to have to check every update that the pickup beats are less than 1?


public class Trap : MonoBehaviour {

    // The amount of beats this trap will wait before starting its cycle.
    public int pickUpBeats;

    // The a String comprised of 0's and 1's that tells the trap the pattern to active/deactivate.
    public string beatCode; //ex: 0101

    // The current beat we're on.
    private int beat;

    // A bool to check if the beat has changed by comparing this to the beatState.
    private bool lastBeatState;

    // The animator for this trap.
    private Animator anim;

    // The name of condition in the animator to change the animation.
    private string animStateName;

    // The reference to the object with this trap's "dangerous" collider.
    // There are two colliders per trap, the actual trap and the dangerous part. A single object 
    // shouldn't have two colliders.
    public GameObject trapCollider;

    // The hitbox that is dangerous to the player. 
    private BoxCollider2D hitboxDanger;

    private AudioSource trapAudio;

    private void Awake()
    {
        lastBeatState = BeatManager.beatState; // does this initialize or create a reference?
        beat = 1;
        anim = GetComponent<Animator>();
        animStateName = "isTriggered";

        hitboxDanger = trapCollider.GetComponent<BoxCollider2D>();
        trapAudio = gameObject.GetComponent<AudioSource>();
    }
	
	void Update () {
        // if beatState changes
        // and if the beat is less than or equal to the length of the beatCode
        // check the beatCode's char at beat-1 position (bc beat starts at 1) and
        // if 1, activate, and if 0, decativate.
        // else if beat now becomes greater than length of beatCode,
        // reset beat back to 1 and check position again.

        if (lastBeatState != BeatManager.beatState)
        {
            // Checks for pickup beats before doing anything.
            if (pickUpBeats < 1)
            {
                // Resets beat back to 1 if the beatCode length is exceeded.
                if (beat > beatCode.Length)
                {
                    beat = 1;
                }

                if (beatCode[beat - 1] == '1')
                {
                    TriggerTrap(true);
                }
                else if (beatCode[beat - 1] == '0')
                {
                    TriggerTrap(false);
                }
                beat++;
            }
            else
            {
                pickUpBeats--;
            }
            lastBeatState = BeatManager.beatState;

        }

        // Constantly checks for one shot animations, as in if the animation finished for
        // an "active" state, we should end the triggered trap early.
        /*
        if (trapAnimationIsActive && WeFinishedTrapAnimation) {
            TriggerTrap(false);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName(animActiveStateAnimationName) && )
        */



    }

    // Ensures that if the trap is ever destroyed, that it'll also kill the parent/collider.
    void OnDestroy() {
        Destroy(transform.parent.gameObject);
    }

    // Given a bool that represents the state of the trap,
    // Will change the animation and enable/disable the collider accordingly.
    private void TriggerTrap(bool state)
    {
        anim.SetBool(animStateName, state);
        hitboxDanger.enabled = state;
        if (state)
        {
            trapAudio.PlayOneShot(trapAudio.clip, 1.0f);
        }
    }

    // The purpose of explicitly making a function to disable the trap is
    // necessary for the animation event to work when the "active" animation
    // ends, because animation events cannot take booleans.
    public void DeactivateEarly()
    {
        TriggerTrap(false);
    }
}
