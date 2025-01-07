using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideFatBird : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bird collides with the player
        if (other.CompareTag("Player"))
        {
            // Get the BirdEnemy component from the current GameObject
            BirdEnemy bird = GetComponent<BirdEnemy>();
            if (bird == null)
            {
                Debug.LogError("BirdEnemy component not found on the bird!");
                return; // Exit if the component is missing
            }

            // Get the PlayerHealth component from the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Get the PlayerMovement component and check if the player is sliding
                PlayerMovement playerController = other.GetComponent<PlayerMovement>();
                if (playerController != null && !playerController.isSliding)
                {
                    // Deal damage to the player
                    playerHealth.TakeDamage(bird.damageAmount);
                    Debug.Log("Player hit by bird and took damage!");
                }
            }
        }
    }
}
