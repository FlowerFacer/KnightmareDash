using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story1 : MonoBehaviour
{
    // Method to start the game
    public void PlayScene2()
    {
        // Load the first level or game scene
        SceneManager.LoadScene("StoryScene2"); // Replace "GameScene" with your game scene name
    }

    // Method to quit the game
    public void SkipScenes()
    {
        // Load the first level or game scene
        SceneManager.LoadScene("LoadingScene"); // Replace "GameScene" with your game scene name
    }
}
