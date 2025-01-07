using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public void StartTheGame()
    {
        // Load the first level or game scene
        SceneManager.LoadScene("InGame"); // Replace "GameScene" with your game scene name
    }
}
