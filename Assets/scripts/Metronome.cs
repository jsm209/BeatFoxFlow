using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Metronome : MonoBehaviour {

    public Sprite middle;
    public Sprite left;
    public Sprite right;

    private Image image;
    private bool wentLeft;
    private bool lastBeatState;

	void Awake () {
        wentLeft = true;
        lastBeatState = BeatManager.beatState;
        image = GetComponent<Image>();
	}
	
	void Update () {
        if (BeatManager.valid) {
            image.sprite = middle;
        } else if (lastBeatState != BeatManager.beatState) {
            Tick();
            lastBeatState = BeatManager.beatState;
        }    
	}

    private void Tick() {
        if (wentLeft) {
            image.sprite = right;
            wentLeft = false;
        } else {
            image.sprite = left;
            wentLeft = true;
        }

    }
}
