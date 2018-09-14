using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requirments for an enemy:
// Animator with "isMoving" variable
// 

public class Enemy : MonoBehaviour {

    [HideInInspector] public bool facingRight;
    [HideInInspector] public float step;

    public GameObject target;
    public float speed;

    private CircleCollider2D detectionArea;
    [HideInInspector] public bool foundPlayer;
    

    private void Awake() {
        foundPlayer = false;
        facingRight = true;
        step = speed * Time.deltaTime;
    }

    public void Update() {
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 0 &&
            Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) != 0 || foundPlayer) {
            GetComponent<Animator>().SetBool("isMoving", true);
        } else {
            GetComponent<Animator>().SetBool("isMoving", false);
        }
        FixDirection();

    }

    // Will make the enemy face the correct direction depending on their movement.
    public void FixDirection() {
        if (GetComponent<Rigidbody2D>().velocity.x > 0 && !facingRight) {
            Flip();
        } else if (GetComponent<Rigidbody2D>().velocity.x < 0 && facingRight) {
            Flip();
        }
    }

    // Will flip the enemy's sprite, and change the stored direction the enemy is facing.
    private void Flip() {
        GetComponent<SpriteRenderer>().flipX = facingRight;
        facingRight = !facingRight;
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Player") {
            foundPlayer = true;
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        if (other.tag == "Player") {
            foundPlayer = false;
        }
    }


}
