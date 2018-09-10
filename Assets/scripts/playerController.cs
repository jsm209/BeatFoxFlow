using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO LIST:
// Make check for player's direction to flip sprite one if statement outside of the movement... 
// I did this because I didn't know how to reference the player's direction. 

public class playerController : MonoBehaviour {

    public LayerMask layer;

    private Rigidbody2D rb;
    private SpriteRenderer playerSprite; // The sprite of the player.
    private Animator anim; // The animator for the player.

    // State and direction
    [SerializeField] private float moveSpeed; // The speed the player travels.
    private bool facingRight = true;     // For determining which way the player is currently facing.
    public float invulnerabilitySeconds; // How many seconds after being hit that the player has iframes.
    private bool fastFalling;

    // Jumping
    [SerializeField] private float jumpForce; // force player jumps with
    public float groundCheckLength; // How far above the ground until the player is "touching" the ground.
    public int vulnerability; // A multiplier for how far the player flies after being hit.
    public int maxJumps; // How many jumps the player has before having to touch the ground.
    private int jumpCount; // How many times the player has jumped already.

    // Knockback
    public float knockback; // Amount of "force" of knockback applied for the duration of knockback.
    public float knockbackLength; // A constant that tells player how much to be knocked back by every time.
    public float knockbackCount; // The current time remaining for knockback state. (player has no control)
    private bool knockbackFromRight; // Denotes the direction of knockback.

    private void Awake()
    {
        // Setting up references...
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // ...
        jumpCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (knockbackCount <= 0)
        {
            DoMovement();
        } else {
            if (knockbackFromRight)
            {
                rb.velocity = new Vector2(-knockback, knockback);
            } else {
                rb.velocity = new Vector2(knockback, knockback);
            }
            knockbackCount -= Time.deltaTime;
        }

        if (rb.velocity.y < -jumpForce) 
        {
            fastFalling = true;
        } else {
            fastFalling = false;
        }

        SetAnim();
    }

    // Checks if any keys are pushed, and if so, will apply the corresponding movement.
    private void DoMovement ()
    {

        // Checking if the player is pushing the correct key to move left.
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            if (facingRight)
            {
                Flip();
            }
        }

        // Checking if the player is pushing the correct key to move right.
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            if (!facingRight)
            {
                Flip();
            }
        }

        // If both movement keys have been released and we're on the ground,
        // immediately stop moving.
        if ((Input.GetKeyUp(KeyCode.D) || (Input.GetKeyUp(KeyCode.A))) && Grounded())
        {
            rb.velocity = new Vector2(0, 0);
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && (jumpCount < maxJumps))
        {
            if (BeatManager.valid) {
                Jump();
                jumpCount++;
            }
            
        }

        if ((Input.GetKeyDown(KeyCode.S)) && !Grounded()) 
        {
            if (BeatManager.valid) {
                FastFall();
            }
            
        }

        
    }

    // Checks when the player collides with another collider, and does several things:
    // If an enemy, will take damage and do knockback. 
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            if (fastFalling) 
            {
                Destroy(other.gameObject);
            }
            else 
            {
                Hurt();
            }
            
        }


        if (transform.position.x < other.transform.position.x)
        {
            knockbackFromRight = true;
        } else {
            knockbackFromRight = false;
        }

        // Reset jumpCount back to zero for more jumps no matter what we collided with.
        jumpCount = 0;
    }

    // Will flip the player's sprite, and change the stored direction the player is facing.
    private void Flip ()
    {
        playerSprite.flipX = facingRight;
        facingRight = !facingRight;
    }

    // Will return a variable that is true if the player is very close to the ground and false otherwise.
    private bool Grounded ()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, layer);
    }

    // Makes the player jump.
    private void Jump ()
    {
        rb.velocity = new Vector3(0, jumpForce, 0);
    }

    // Makes the player fastfall
    private void FastFall() {
        rb.velocity = new Vector3(0, -jumpForce * 2, 0);
    }

    // Checks the movement of the player and sets their animation accordingly.
    private void SetAnim()
    {
        if (!Grounded())
        {
            anim.SetBool("isJumping", true);
        } else {
            anim.SetBool("isJumping", false);
            // Player will use walking animation if they have only horizontal movement and are grounded.
            if (Mathf.Abs(rb.velocity.x) > 0 && Mathf.Abs(rb.velocity.y) == 0)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        }

        
    }

    private void Hurt() {
        if (gameObject.GetComponent<playerHeartTracker>().health > 0) {
            ChangeHearts(-1);
            StartCoroutine("Invulnerable");
            knockbackCount = knockbackLength;
        }
        Debug.Log("Hurt");
    }

    // Given an integer,
    // Will change the player's heart tracker by the given amount.
    public void ChangeHearts(int n)
    {
        gameObject.GetComponent<playerHeartTracker>().ChangeHealth(n);
    }

    // Will kill the player.
    public void KillPlayer()
    {
        Debug.Log("PLAYER DIED.");
    }

    // Makes the player unable to collide with "enemy" layer, as well as turn 
    // partially transparent for a given amount of time.
    IEnumerator Invulnerable()
    {
        Debug.Log("PLAYER IS INVULNERABLE");
        Physics2D.IgnoreLayerCollision(13, 16, true);
        gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        yield return new WaitForSeconds(invulnerabilitySeconds);
        gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        Physics2D.IgnoreLayerCollision(13, 16, false);
        yield return null;
    }
}
