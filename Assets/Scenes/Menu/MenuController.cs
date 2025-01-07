using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Method to start the game
    public void PlayGame()
    {
        // Load the first level or game scene
        SceneManager.LoadScene("StoryScene1"); // Replace "GameScene" with your game scene name
    }

    // Method to quit the game
    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit(); // Works only in builds, not in the editor
    }
}
