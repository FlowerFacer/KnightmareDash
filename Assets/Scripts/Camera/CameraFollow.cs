using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Player to follow
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement
    public float verticalFollowFactor = 1f; // Fraction of vertical movement the camera should follow
    public float verticalOffset = 1f; // Offset to make the camera start higher

    private Vector3 initialOffset; // Initial offset between the camera and the player
    private float baseY; // Base Y position of the camera

    void Start()
    {
        // Calculate the initial offset
        initialOffset = transform.position - player.position;

        // Store the camera's base Y position with the additional offset
        baseY = transform.position.y + verticalOffset;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Calculate the desired Y position for the camera
            float playerVerticalDifference = player.position.y - (baseY - initialOffset.y);
            float desiredY = baseY + playerVerticalDifference * verticalFollowFactor;

            // Ensure the camera's X and Z positions remain unchanged
            Vector3 desiredPosition = new Vector3(
                player.position.x + initialOffset.x,
                desiredY,
                transform.position.z
            );

            // Smoothly move the camera to the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
