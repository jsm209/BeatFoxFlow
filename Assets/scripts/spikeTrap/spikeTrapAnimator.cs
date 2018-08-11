using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeTrapAnimator : MonoBehaviour {

    private Animator anim; // The anim is the animator for the spikes.

    private void Awake ()
    {
        // Setting up references...
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D()
    {
        anim.SetBool("isTriggered", true);
    }

    private void OnTriggerExit2D()
    {
        anim.SetBool("isTriggered", false);
    }
}
