using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public int damageAmount = 1; // Damage dealt by the trap
    private bool canDamage = false; // Prevents damage at the start

    void Start()
    {
        // Delay damage for 1 second after the game starts
        Invoke(nameof(EnableDamage), 1f);
    }

    private void EnableDamage()
    {
        canDamage = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (canDamage && collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

            // Optional: Knock the player back
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.AddForce(new Vector2(-400f, 300f)); // Knock back effect
            }
        }
    }
}
