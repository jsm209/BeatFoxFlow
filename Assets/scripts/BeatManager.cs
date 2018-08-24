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

    // The current bpm to send signals at.
    public float bpm;

    // What the current bpm is in seconds.
    private float bpmInSeconds;

    // Tracks the total number of beats so far.
    private float currentBeat; 

    private void Awake()
    {
        bpmInSeconds = TIME_PER_MINUTE / bpm;
        currentBeat = 0f;
        beatState = false;
    }

    private void Start()
    {
        InvokeRepeating("SendBeat", 0f, bpmInSeconds);
    }

    // Will increment the currentBeat counter by 1, Log the current count and whether the beat is 
    // active or not, and toggle the beatState. Used to "tick" the other functions of the game to
    // be in rhythm.
    void SendBeat() {
        currentBeat++;
        Debug.Log("Sent beat " + currentBeat + " and beat is " + beatState);
        beatState = !beatState;
    }
}
