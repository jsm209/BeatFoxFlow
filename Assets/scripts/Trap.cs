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

    private void Awake()
    {
        lastBeatState = BeatManager.beatState; // does this initialize or create a reference?
        beat = 1;
        anim = GetComponent<Animator>();
        animStateName = "isTriggered";

        hitboxDanger = trapCollider.GetComponent<BoxCollider2D>();
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
            if (pickUpBeats < 1)
            {
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


	}

    // Given a bool that represents the state of the trap,
    // Will change the animation and enable/disable the collider accordingly.
    private void TriggerTrap(bool state)
    {
        anim.SetBool(animStateName, state);
        hitboxDanger.enabled = state;
    }
}
