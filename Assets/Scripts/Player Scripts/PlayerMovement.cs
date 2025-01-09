using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float runSpeed = 5f;
    [SerializeField] public float jumpForce = 7f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool isJumping = false;
    public bool isSliding = false;

    private BoxCollider2D boxCollider;
    private Vector2 originalSize;
    private Vector2 slideSize = new Vector2(1.0f, 0.4f);
    private Vector2 originalOffset;
    private Vector2 slideOffset = new Vector2(0, -0.2f);

     private PlayerControls controls;
    [SerializeField] private LayerMask groundLayer; // For more accurate ground detection

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Slide.performed += ctx => StartSlide();
    }

    void OnEnable()
    {
        if (!PauseManager.isPaused) // Only enable controls if not paused
        {
            controls.Gameplay.Enable();
        }
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        if (boxCollider != null)
        {
            originalSize = boxCollider.size;
            originalOffset = boxCollider.offset;
        }
    }

    void FixedUpdate()
    {
        if (PauseManager.isPaused) return; // Skip updating while paused

        rb.velocity = new Vector2(runSpeed, rb.velocity.y); // Auto-run logic
    }

    public void Jump()
    {
        if (EventSystem.current.IsPointerOverGameObject() && isGrounded && !isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            if (animator != null)
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isRunning", false);
            }

            Debug.Log("Jump Started");
        }
    }

    public void StartSlide()
    {
        if (isGrounded && !isSliding) // Ensure the player is grounded and not already sliding
        {
            isSliding = true;

            // Adjust the Box Collider for sliding
            if (boxCollider != null)
            {
                boxCollider.size = slideSize; // Shrink the collider size
                boxCollider.offset = slideOffset; // Adjust the offset to match the bottom of the sprite
            }

            // Temporarily disable vertical movement (freeze Y-position)
            if (GetComponent<Rigidbody2D>() != null)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }

            // Play the sliding animation
            if (animator != null)
            {
                animator.SetBool("isSliding", true);
                animator.SetBool("isRunning", false);
            }

            Debug.Log("Slide Started");
        }
    }

    public void StopSlide()
    {
        if (isSliding)
        {
            isSliding = false;

            // Restore the original Box Collider size and offset
            if (boxCollider != null)
            {
                boxCollider.size = originalSize;
                boxCollider.offset = originalOffset;
            }

            // Restore Rigidbody2D constraints
            if (GetComponent<Rigidbody2D>() != null)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; // Allow full movement again
            }

            // Reset sliding animation
            if (animator != null)
            {
                animator.SetBool("isSliding", false);
                animator.SetBool("isRunning", true);
            }

            Debug.Log("Slide Stopped");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;

            if (animator != null)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isRunning", true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
