using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour
{
    [SerializeField]
    public float speed = 6f; // Flying speed of each birb
    public int damageAmount = 1; // Damage dealt to the player by the birb
    public bool shouldFlip = true; // Controls if the sprite should flip 

    private Rigidbody2D rb; // Rigidbody reference
    private Animator anim; // Animator refrence
    private SpriteRenderer spriteRenderer; // SpriteRenderer reference

    public AudioClip birdChirp; // Unique chirp sound for each birb
    private bool hasChirped = false;
    private float chirpCooldown = 2f; // Cooldown in seconds
    private float lastChirpTime = 0f;

    void Update()
    {
        if (spriteRenderer != null && spriteRenderer.isVisible && !hasChirped)
        {
            PlayChirp();
            hasChirped = true;
        }
        else if (spriteRenderer != null && !spriteRenderer.isVisible)
        {
            hasChirped = false;
        }

        // Destroy the bird if it goes off-screen to the left
        if (spriteRenderer != null && !spriteRenderer.isVisible && transform.position.x < Camera.main.transform.position.x - 10f)
        {
            Destroy(gameObject);
            Debug.Log("Birb has been eliminated! hahaha");
        }
    }

    void PlayChirp()
    {
        if (birdChirp != null && Time.time > lastChirpTime + chirpCooldown)
        {
            AudioManager.instance.PlaySound(birdChirp);
            lastChirpTime = Time.time;
        }
    }

    void Start()
    {


        // Get the RigidBody2D, Animator and SpriteRenderer components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensures the birb is not affected by gravityyyy
        if (rb != null)
        {
            rb.gravityScale = 0;
        }

        // Flips the birb to face left
        if (shouldFlip)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = true; // This flips the sprite horizontally
            }
        }

        // Plays the birb flying animation
        if (anim != null)
        {
            anim.SetTrigger("Fly");
        }
    }

    void FixedUpdate()
    {
        // Moves the birbs to the left 
        transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
