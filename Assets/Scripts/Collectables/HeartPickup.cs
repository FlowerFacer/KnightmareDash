using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class HeartPickup : MonoBehaviour
{
    public int healAmount = 1;
    public AudioClip HeartCollect; // Ref to the heart sound effect
    private ParticleSystem particles;

    void Start()
    {
        // Find the Particle System attached to this object
        particles = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checks if the object that entered the trigger is the player object
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount); // Heals the player object
                AudioManager.instance.PlaySound(HeartCollect);

                // Play the particle effect
                if (particles != null)
                {
                    particles.transform.parent = null; // Detach the Particle System
                    particles.Play();

                    Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constantMax); // Clean up
                }

                // Destroys the heart object
                Destroy(gameObject);
            }
        }
    }
}
