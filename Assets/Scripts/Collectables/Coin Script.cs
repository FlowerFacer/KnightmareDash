using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int coinValue = 1; // Value of the coin
    public AudioClip coinSound; // Ref to the coin sound effect

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Entered by: " + other.gameObject.name); // Debug log to verify the collision

        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Add the coin value to the player's score
            ScoreTracker.instance.AddCoinScore(coinValue);

            // Plays a sound of coin being collected
            if (coinSound != null)
            {
                AudioManager.instance.PlaySound(coinSound);
            }

            Debug.Log("Coin collected!");

            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}
