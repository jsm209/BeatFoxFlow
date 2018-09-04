using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartPickup : MonoBehaviour {

    public int healAmount;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<playerController>().ChangeHearts(healAmount);
            Destroy(this.gameObject);
        }
        
    }
}
