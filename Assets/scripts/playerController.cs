using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO LIST:
// Make check for player's direction to flip sprite one if statement outside of the movement... 
// I did this because I didn't know how to reference the player's direction. 

public class playerController : MonoBehaviour {

    public LayerMask layer;

    private Rigidbody2D rb;
    private Transform tf;
    private SpriteRenderer playerSprite; // The sprite of the player.
    private Animator anim; // The animator for the player.

    [SerializeField] private float moveSpeed; // The speed the player travels.
    private bool facingRight = true;     // For determining which way the player is currently facing.
    [SerializeField] private float jumpForce;
    public float groundCheckLength;
    public int vulnerability; // A multiplier for how far the player flies after being hit.

    // Knockback
    public float knockback; // Amount of "force" of knockback applied for the duration of knockback.
    public float knockbackLength; // A constant that tells player how much to be knocked back by every time.
    public float knockbackCount; // The current time remaining for knockback state. (player has no control)
    private bool knockbackFromRight; // Denotes the direction of knockback.
    

    private void Awake()
    {
        // Setting up references...
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        playerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
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
        
        setAnim();
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

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && Grounded())
        {
            Jump();
        }
    }

    // Checks when the player collides with another collider, and does several things:
    // If an enemy, will take damage and do knockback. 
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "enemy")
        {
            
            knockbackCount = knockbackLength;
            Debug.Log("Hurt");
        }

        if (transform.position.x > other.transform.position.x)
        {
            knockbackFromRight = true;
        } else {
            knockbackFromRight = false;
        }
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

    // Checks the movement of the player and sets their animation accordingly.
    private void setAnim()
    {
        // Player will use walking animation if they have only horizontal movement.
        if (Mathf.Abs(rb.velocity.x) > 0 && Mathf.Abs(rb.velocity.y) == 0)
        // if (Input.GetAxis("Horizontal") > 0)
        {
            anim.SetBool("isWalking", true);
        } else {
            anim.SetBool("isWalking", false);
        }
        
    }
}
