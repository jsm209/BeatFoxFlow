using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipRobin : MonoBehaviour {

    private Animator anim;
    public GameObject text;


	// Use this for initiali    zation
	void Start () {
        anim = GetComponent<Animator>();
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        anim.SetBool("active", true);
        text.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        anim.SetBool("active", false);
        text.SetActive(false);
    }
}
