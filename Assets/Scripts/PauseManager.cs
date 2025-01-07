using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // Assign the PauseMenu Canvas in the Inspector
    public GameObject pauseButton; // Assign the PauseButton GameObject in the Inspector
    public static bool isPaused = false;

    private void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // Ensure the pause menu is hidden at the start
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pause the game
        pauseMenu.SetActive(true); // Show pause menu
        pauseButton.SetActive(false); // Hide pause button
    }

    public void ResumeGame()
    {
        isPaused = false;

        // Clear input buffer to prevent unwanted actions
        Input.ResetInputAxes();

        pauseMenu.SetActive(false); // Hide the pause menu
        pauseButton.SetActive(true); // Show the pause button
        Time.timeScale = 1f; // Resume the game
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Reset time scale before loading a new scene
        SceneManager.LoadScene("MenuScene"); // Replace "MainMenu" with the name of your main menu scene
    }
}
