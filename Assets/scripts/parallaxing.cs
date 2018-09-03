using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script calculates the amount of parallax to apply to a distant background or closer 
// foreground, based on the distance moved by the camera, and how close the back/foreground
// is on the z-axis.

// One possible improvement to make is to also do the same calculation for the y-axis, as
// this only currently applies parallaxing to the x-axis.

public class parallaxing : MonoBehaviour
{

    // Array of all back and foregrounds to be parallaxed.
    public Transform[] backgrounds;

    // Proportion of camera's movement to move the backgrounds by.
    private float[] parallaxingScales;

    // How smooth the parallax is going to be. Make sure to set above 0.
    public float smoothing = 1f;

    // Reference to the main camera's transform.
    private Transform cam;

    // Stores the position of the camera from the previous frame.
    private Vector3 previousCamPosition;


    // Is called before Start(). Good time to establish references.
    private void Awake()
    {
        // set up the camera reference.
        cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start()
    {
        // the previous frame had the current frame's camera position
        previousCamPosition = cam.position;

        // Assigning corresponding parallax scales...
        parallaxingScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxingScales[i] = backgrounds[i].position.z * -1; // -1 or else we'll get an opposite effect
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // the parallax is the opposite of the camera movement, and proportional to distance moved.
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxingScales[i];

            // Set a target x position which is the current position plus the parallax amount.
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // Create a target position which is the background's current position with it's target x position.
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // fade between current position and the target position using lerp.
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // update the cam's position
        previousCamPosition = cam.position;
    }
}