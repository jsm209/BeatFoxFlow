using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGmusic : MonoBehaviour {

    // A bool to check if the beat has changed by comparing this to the beatState.
    private bool lastBeatState;

    // The current beat we're on.
    private int beat;

    private AudioSource music;

    private bool currentlyInALoop;

    private void Awake()
    {
        lastBeatState = BeatManager.beatState;
        currentlyInALoop = false;
        music = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (lastBeatState != BeatManager.beatState && !currentlyInALoop)
        {
            currentlyInALoop = true;
            music.Play(0);
        }
	}
}
