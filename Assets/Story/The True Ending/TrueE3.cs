using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrueE3 : MonoBehaviour
{
    // Method to start the game
    public void ExitRealHome()
    {
        // Load the first level or game scene
        SceneManager.LoadScene("MenuScene"); // Replace "GameScene" with your game scene name
    }
}
