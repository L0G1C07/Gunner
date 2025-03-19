using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float horizontalInput;
    public float movementSpeed = 10f;
    public float jumpPower = 10f;
    public float wallJumpPowerX = 8f; // Horizontal force for wall jump
    public float wallJumpPowerY = 12f; // Vertical force for wall jump
    bool isJumping = false;
    bool isTouchingWall = false;
    bool isFacingRight = true; // Track player's direction

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public AudioClip jumpSound; // Assign in the Inspector
    private AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // Use GetAxisRaw for instant input response

        // Flip the player when changing direction
        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }

        // Normal Jump
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;

            if (jumpSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }

        // Wall Jump
        if (Input.GetButtonDown("Jump") && isTouchingWall)
        {
            rb.velocity = new Vector2(-horizontalInput * wallJumpPowerX, wallJumpPowerY);
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);

        // Ensure animation remains in walking state while moving
        animator.SetBool("isWalking", Mathf.Abs(horizontalInput) > 0.01f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jump when touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        // Detect walls for wall jumping (Make sure walls have the "Wall" tag)
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
            isJumping = false; // Allow jumping off walls
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // When player leaves the wall, reset flag
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f); // Flip the player
    }
}
