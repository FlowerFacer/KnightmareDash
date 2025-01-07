using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    public int gemValue = 1; // Value of each gem
    public AudioClip gemCollectSound; // Sound effect for gem pickup
    public RainbowTextEffect rainbowText; // Reference to the RainbowText script

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checks if the player gameobject is collecting the gem
        if (other.CompareTag("Player"))
        {
            ScoreTracker.instance.AddGemScore(gemValue);

            // Plays the gem collection sound
            if (gemCollectSound != null)
            {
                AudioManager.instance.PlaySound(gemCollectSound);
            }

            // Display the rainbow text
            if (rainbowText != null)
            {
                rainbowText.ShowText($" | {ScoreTracker.instance.gemScore} |");
            }

            Destroy(gameObject);
        }
    }
}
