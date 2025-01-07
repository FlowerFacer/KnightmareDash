using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    public Animator chestAnimator; // ref to the chest anim

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the chest opening anim
            if (chestAnimator != null)
            {
                chestAnimator.SetTrigger("Open");
            }
        }
    }
}
