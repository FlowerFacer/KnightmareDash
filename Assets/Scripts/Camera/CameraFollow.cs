using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Player to follow
    public float smoothSpeed = 0.125f;
    private Vector3 initialOffset;

    void Start()
    {
        initialOffset = transform.position - player.position;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + initialOffset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
