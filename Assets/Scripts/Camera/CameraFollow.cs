using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Player to follow
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement
    public Vector2 minBounds; // Minimum bounds for the camera
    public Vector2 maxBounds; // Maximum bounds for the camera
    public float pixelsPerUnit = 100f; // Your sprites' Pixels Per Unit value

    public float verticalFollowFactor = 0.5f; // Fraction of vertical movement the camera should follow
    public float verticalOffset = 0.1f; // Offset to make the camera start higher

    private Vector3 initialOffset; // Initial offset between the camera and the player
    private float baseY; // Base Y position of the camera

    void Start()
    {
        // Set orthographic size for the camera based on screen size and pixels per unit
        Camera.main.orthographicSize = Screen.height / (2.0f * pixelsPerUnit);

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

            // Calculate the desired position for the camera
            Vector3 desiredPosition = new Vector3(
                player.position.x + initialOffset.x,
                desiredY,
                transform.position.z
            );

            // Clamp the desired position within the bounds
            float clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
            float clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

            // Smoothly move the camera to the clamped position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(clampedX, clampedY, transform.position.z), smoothSpeed);

            transform.position = smoothedPosition;
        }
    }
}
