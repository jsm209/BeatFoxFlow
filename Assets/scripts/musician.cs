using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musician : MonoBehaviour {

    // Allows user to give object particular sounds.
    /*
    public AudioClip I = new AudioClip();
    public AudioClip II = new AudioClip();
    public AudioClip III = new AudioClip();
    public AudioClip IV = new AudioClip();
    public AudioClip V = new AudioClip();
    public AudioClip VI = new AudioClip();
    public AudioClip VII = new AudioClip();
    */

    private AudioSource currentTone;

    // The composer determines the progression for the musician to play.
    // public GameObject composer;

    // How many beats are in one progression (Should be 4).
    public int beatsPerProgression;

    // Holds all the sounds
    public AudioClip[] sounds;

    // Needed to check if the beat has changed or not.
    private bool lastBeatState;

    // Tracks the progress through a particular progression.
    private int currentBeat;
    
    // A queue of musical progressions, assumed to have 2d arrays.
    private Queue score;

    // The current progression we're on.
    private int[][] currentProgression;

    private void Awake() {
        
        //sounds = new AudioClip[7];
        score = new Queue();
        currentTone = gameObject.GetComponent<AudioSource>();
    }


    void Start () {
        // Set the last beat state to the current beat state.
        lastBeatState = BeatManager.beatState;
        /*
        // Add all given sounds to an array
        sounds[0] = I;
        sounds[1] = II;
        sounds[2] = III;
        sounds[3] = IV;
        sounds[4] = V;
        sounds[5] = VI;
        sounds[6] = VII;
        */

        // initial state
        currentBeat = 1;
        UpdateScore();
        currentProgression = (int[][])score.Dequeue();
    }
	
	// Update is called once per frame
	void Update () {
        if (lastBeatState != BeatManager.beatState) {
            // Debug.Log("Now playing a progression.");
            PlayProgressionBeat(currentBeat - 1, currentProgression);
            currentBeat++;
            if (currentBeat > beatsPerProgression) {
                // Debug.Log("Resetting to new progression.");
                currentBeat = 1;
                UpdateScore();
                currentProgression = (int[][])score.Dequeue();
            }
        }

        lastBeatState = BeatManager.beatState;
        
	}

    // Given the current beat and the curren progression,
    // Will play all the notes located at that beat in the progression.
    private void PlayProgressionBeat(int beat, int[][] progression) {
        // Will this for loop create an arppegiated effect if I'm trying to
        // play chords?
        for (int i = 0; i < progression[beat].Length; i++) {
            // "0th" degrees will be treated as rests.
            if ((progression[beat][i]) != 0) {
                // "-1" accounts for difference between index and music interval.
                // Debug.Log("Current tone clip should be " + sounds[progression[beat][i] - 1]);
                currentTone.PlayOneShot(sounds[progression[beat][i] - 1]);
            }
        }
    }

    // Ask composer for two progressions to put into the score.
    private void UpdateScore() {
        while (score.Count <= 2) {
            int[][] newProgression = gameObject.GetComponent<Composer>().getProgression();
            score.Enqueue(newProgression);
            // Debug.Log("Enqueued new progression: " + newProgression.ToString());
        }
    }
}
