using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartPickup : MonoBehaviour {

    public int healAmount;
    public AudioClip pickupSound;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<playerController>().ChangeHearts(healAmount);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(this.gameObject);
        }
        
    }
}
