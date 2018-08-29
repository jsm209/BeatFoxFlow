using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHeartTracker : MonoBehaviour {

    // The amount of health object currently has.
    public int health;

    // The maximum number of heart containers possible.
    public int numOfHearts;

    // An array of the maximum number of hearts.
    public Image[] hearts;
    
    // Image for when a health point still exists.
    public Sprite fullHeart;

    // Image for when a health point has been lost.
    public Sprite emptyHeart;

    // Given an integer,
    // Will change the amount of health by that amount, and kills the object
    // when their health goes below or reaches 0.


    private void Update() {

        // Insures health does not exceed maximum number of heart containers.
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        // Draws the correct amount of full and empty hearts based on current health.
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }

            // Draws the correct amount of maximum hearts.
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void ChangeHealth(int n)
    {
        health += n;
        if (health <= 0)
        {
            health = 0;
            gameObject.GetComponent<playerController>().KillPlayer();
        }
    }

}
        