using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story4 : MonoBehaviour
{
    // Method to start the game
    public void StartAdventure()
    {
        // Load the first level or game scene
        SceneManager.LoadScene("LoadingScene"); // Replace "GameScene" with your game scene name
    }
}
