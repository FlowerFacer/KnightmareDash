using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinCheckpoint : MonoBehaviour
{
    public GameObject player; // Reference to the player

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the win sequence on the player
            var winController = player.GetComponent<PlayerWinController>();
            if (winController != null)
            {
                winController.TriggerWin();
            }
        }
    }
}
