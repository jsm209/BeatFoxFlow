using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// A BeatManager is capable of accepting a bpm, then performing functions by oscillating at that
// particular bpm.

public class BeatManager : MonoBehaviour {

    // Length of time in one minute (60 seconds in 1 minute, 60,000ms in 1 minute)
    static private float TIME_PER_MINUTE = 60;

    // Whether the current beat is an off or on beat, true being on beat and false being off beat.
    [HideInInspector] static public bool beatState;

    // Tracks if the current beat right now is valid or not. Intended to check if things are timed
    // on beat.
    [HideInInspector] static public bool valid;

    // The time before and after the beat that the beat remains valid for action.
    public float validBufferPre;
    public float validBufferPost;

    // The current bpm to send signals at.
    public float bpm;

    // What the current bpm is in seconds.
    private float bpmInSeconds;

    // Tracks the total number of beats so far.
    [HideInInspector] public float currentBeat; 

    private void Awake()
    {
        bpmInSeconds = TIME_PER_MINUTE / bpm;
        currentBeat = 0f;
        beatState = false;
        valid = false;
    }

    private void Start()
    {
        StartCoroutine(SendBeat());
    }

    // Will increment the currentBeat counter by 1, Log the current count and whether the beat is 
    // active or not, and toggle the beatState. Used to "tick" the other functions of the game to
    // be in rhythm.
    IEnumerator SendBeat() {
        while (true) {
            // Allows player to take a valid action slightly before the beat.
            valid = true;
            yield return new WaitForSeconds(validBufferPre);

            // Do beat
            currentBeat++;
            //Debug.Log("Sent beat " + currentBeat + " and beat is " + beatState);
            beatState = !beatState;

            // Allows player to take a valid action slightly after the beat.
            yield return new WaitForSeconds(validBufferPost);
            valid = false;

            // Accounts for the buffer time before waiting correct time according to bpm.
            yield return new WaitForSeconds(bpmInSeconds - (validBufferPre + validBufferPost));
        }
        
        

    }

    // To fix, make a coroutine that:
    // allows valid to be true for validBuffer time after beat is sent
    // allows valid to be true for validBuffer time before beat is sent
}
