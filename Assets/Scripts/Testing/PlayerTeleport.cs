using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public Transform player; // Ref to the player object
    public Vector3 TargetPosition; // The position to teleport the player to

    public void TeleportPlayer()
    {
        if (player != null)
        {
            player.position = TargetPosition;
            Debug.Log($"Player teleported to:  {TargetPosition}");
        }
        else
        {
            Debug.LogWarning("Player reference is missing!");
        }
    }
}
