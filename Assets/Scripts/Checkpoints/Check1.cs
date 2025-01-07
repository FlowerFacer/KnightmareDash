using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check1 : MonoBehaviour
{
    public GameObject[] enemiesToActivate; // Array of enemies to activate
    private bool isActivated = false; // Prevents multiple activations

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player triggered the checkpoint
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true; // Prevent re-triggering
            ActivateEnemies();
        }
    }

    private void ActivateEnemies()
    {
        foreach (GameObject enemy in enemiesToActivate)
        {
            if (enemy != null)
            {
                // Freeze and unfreeze Rigidbody2D
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Unfreeze the Rigidbody2D so the enemy can move
                    rb.constraints = RigidbodyConstraints2D.None;
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Freeze rotation only
                }

                // Enable the movement script (if it exists)
                BirdEnemy birdEnemy = enemy.GetComponent<BirdEnemy>();
                if (birdEnemy != null)
                {
                    birdEnemy.enabled = true;
                }
                else
                {
                    Debug.LogWarning($"{enemy.name} does not have a BirdEnemy script attached!");
                }
            }
        }
    }

    private void Start()
    {
        // Freeze enemies at the start
        foreach (GameObject enemy in enemiesToActivate)
        {
            if (enemy != null)
            {
                // Freeze Rigidbody2D movement
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll; // Completely freeze the enemy
                }

                // Disable movement scripts (e.g., BirdEnemy) at the start
                BirdEnemy birdEnemy = enemy.GetComponent<BirdEnemy>();
                if (birdEnemy != null)
                {
                    birdEnemy.enabled = false;
                }
            }
        }
    }
}
