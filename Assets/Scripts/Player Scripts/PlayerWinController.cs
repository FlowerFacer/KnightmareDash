using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWinController : MonoBehaviour
{
    public Animator playerAnimator; // Reference to the player's Animator
    public Animator chestAnimator; // Reference to the chest's Animator
    public float winYPosition = -0.86f; // Exact Y position during the win animation
    private float originalYPosition; // Store only the original Y position

    private bool hasWon = false; // Prevent multiple triggers

    public AudioClip WinAchieved; // Ref to the win sound effect

    // UI Elements
    public GameObject winPanel; // Reference to the Win Panel
    public TextMeshProUGUI winText; // Reference to the Win Text

    void Start()
    {
        // Save the player's starting position
        originalYPosition = transform.localPosition.y;

        // Ensure the Win Panel and Text are initially hidden
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        if (winText != null)
        {
            winText.enabled = false;
        }
    }

    public void TriggerWin()
    {
        if (hasWon) return; // Prevent multiple triggers
        hasWon = true; // Prevent triggering multiple times

        // Stop player movement and freeze physics
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        AudioManager.instance.PlaySound(WinAchieved);

        // Trigger the win animation
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Win");
        }

        StartCoroutine(OpenChestWithDelay());

        // Adjust the player's height for the win animation
        AdjustForWinAnimation();

        // Show the Win Panel and Text after a short delay
        Invoke(nameof(ShowWinPanel), 2f);
    }

    public void AdjustForWinAnimation()
    {
        // Set the player's Y position to the exact win Y position
        transform.localPosition = new Vector3(transform.localPosition.x, winYPosition, transform.localPosition.z);
    }

    public void EndWinAnimation()
    {
        // test
    }

    private IEnumerator OpenChestWithDelay()
    {
        yield return new WaitForSeconds(3f); // Delay time
        if (chestAnimator != null)
        {
            chestAnimator.SetBool("isOpen", true);
        }
    }

    private void ShowWinPanel()
    {
        // Display the Win Text and Panel
        if (winText != null)
        {
            winText.enabled = true;
        }

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    // Button Methods for the Win Panel
    public void BeHappyWithCat()
    {
        SceneManager.LoadScene("True1"); // Reload the current scene
    }

    public void BeHappyTogether()
    {
        SceneManager.LoadScene("Romantic1"); // Load the Main Menu scene
    }
}
