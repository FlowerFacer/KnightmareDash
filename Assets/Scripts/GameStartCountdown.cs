using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameStartCountdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Ref to the UI text for displaying the countdown
    public int countdownTime = 5; // Countdown starting time in seconds

    public PlayerMovement playerMovement; // Ref to the playerMovement script
    public List<GameObject> enemiesToDisable; // List of enemies to disable

    private Animator playerAnimator; // Player's Animator
    private bool isCountdownActive = false; // Track if countdown is running

    private bool gameStarted = false;

    private void Start()
    {
        isCountdownActive = true;

        // Ensures the player is idle at the start
        if (playerMovement != null)
        {
            playerMovement.runSpeed = 0f; // Stops the player movement
            playerMovement.GetComponent<Animator>().SetBool("isRunning", false);
            playerMovement.GetComponent<Animator>().SetBool("isIdle", true);    
        }

        foreach (GameObject enemy in enemiesToDisable)
        {
            if (enemy != null)
            {
                enemy.SetActive(false);
                Debug.Log($"Disabled enemy: {enemy.name}");
            }
        }

        // Start the countdown coroutine
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        int currentTime = countdownTime;

        while (currentTime > 0)
        {
            // Update the UI countdown text
            if (countdownText != null)
            {
                countdownText.text = currentTime.ToString();
            }

            yield return new WaitForSeconds(1f); // Wait for 1 second
            currentTime--;
        }

        // When the countdown reaches 0
        if (countdownText != null)
        {
            countdownText.text = "GO!!!"; // Show "Go!" for 1 second
        }

        yield return new WaitForSeconds(1f); // Wait for "Go!" to be displayed

        if (countdownText != null)
        {
            countdownText.text = ""; // Clear the countdown text
        }

        StartGame(); // Start the game logic
    }

    private void TriggerPlayerJump()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isJumping", true); // Trigger the jump animation
        }

        // Reset the jumping state after a short delay
        StartCoroutine(ResetJumpState());
    }

    private IEnumerator ResetJumpState()
    {
        yield return new WaitForSeconds(0.5f); // Wait for the jump animation to finish

        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isJumping", false); // Reset the jump animation
        }
    }

    private void StartGame()
    {
        isCountdownActive = false;

        // Resume player movement
        if (playerMovement != null)
        {
            playerMovement.runSpeed = 6.7f; // Restore player movement speed
        }

        // Trigger the jump animation after "GO!"
        TriggerPlayerJump();

        // Resume player movement
        if (playerAnimator != null)
        {
            playerMovement.GetComponent<Animator>().SetBool("isIdle", false);
            playerMovement.GetComponent<Animator>().SetBool("isRunning", true); // Trigger running animation
        }

        foreach (GameObject enemy in enemiesToDisable)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
                Debug.Log($"Disabled enemy: {enemy.name}");
            }
        }

        gameStarted = true;
    }
}
