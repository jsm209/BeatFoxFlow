using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTES FOR FUTURE REFACTORING:
// cycling and animToggle bools could be combined in some way.
// active/pickup/rest beats and their corresponding "inSeconds" counterparts could be reduced.


public class RhythmicTrap : MonoBehaviour {

    private float TIME_PER_MINUTE = 60; // Length of time in one minute (60 seconds in 1 minute, 60,000ms in 1 minute)

    public GameObject trapCollider;
    private BoxCollider2D hitbox;

    private Animator anim; // The anim is the animator for the trap.
    private string animStateName; // The name of condition in the animator to change the animation.
    private bool animToggle; // A boolean that toggles for when the trap is active or not.
    public bool isRhythmic; // Tells the trap if it has to obey the bpm/rhythm to trigger, else it will be proximity triggered
    private bool cycling; // Is this trap currently cycling in it's behavior?

    public float bpm; // The bpm that this trap triggers at.
    public float activeBeats; // How many beats to stay active.
    public float restBeats; // How many beats to be disabled.
    public float pickupBeats; // How much time to wait before actually starting rhythm.

    private float activeBeatsInSeconds; // How many seconds to stay active.
    private float restBeatsInSeconds; // How many seconds to stay inactive.
    private float pickupBeatsInSeconds; // How many seconds to initially wait before cycling.

    private void Awake ()
    {
        // Setting up references and variables...
        hitbox = trapCollider.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        animStateName = "isTriggered";
        animToggle = false;
        cycling = false;
    }

    private void Start()
    {
        // Convert given beats to seconds.
        activeBeatsInSeconds = ConvertBeatsToSeconds(activeBeats);
        restBeatsInSeconds = ConvertBeatsToSeconds(restBeats);
        pickupBeatsInSeconds = ConvertBeatsToSeconds(pickupBeats);

        // Waits initial pickupBeats, if this trap is a rhythmic trap.
        if (isRhythmic)
        {
            PickUp();
        }
    }

    // Given an amount of beats, will convert the beats to seconds depending on 
    // the current bpm and time per minute.
    private float ConvertBeatsToSeconds(float beats)
    {
        return (TIME_PER_MINUTE / bpm) * beats;
    }

    private void Update()
    {
        // If the trap is rhythmic and not currently in a cycle of movement.
        if (isRhythmic && !cycling)
        {
            StartCoroutine("Oscillate");
        }
    }

    private void OnTriggerEnter2D()
    {
        if (!isRhythmic)
        {
            ToggleAnimation();
        }
        
    }

    private void OnTriggerExit2D()
    {
        if (!isRhythmic)
        {
            ToggleAnimation();
        }
    }

    private void ToggleAnimation()
    {
        animToggle = !animToggle;
        anim.SetBool(animStateName, animToggle);
        hitbox.enabled = animToggle;
        Debug.Log("Trap is " + animToggle);
    }

    // Will begin a new oscillation cycle. It toggles the animation to active, 
    // then waits the correct amount of active beats, then toggles to inactive,
    // and again waits the correct amount of beats.
    IEnumerator Oscillate()
    {
        cycling = true;
        ToggleAnimation();
        yield return new WaitForSeconds(activeBeatsInSeconds);
        ToggleAnimation();
        yield return new WaitForSeconds(restBeatsInSeconds);
        cycling = false;
    }

    IEnumerator PickUp()
    {
        yield return new WaitForSeconds(pickupBeatsInSeconds);
        ToggleAnimation();
    }
}
