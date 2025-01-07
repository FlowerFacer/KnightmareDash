using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance;
    public int coinScore = 0; // Player's score
    public int gemScore = 0; // Player's score

    public TMP_Text coinScoreText;
    public TMP_Text gemScoreText;

    private void Awake()
    {
        // Ensures only one instance of ScoreTracker exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to add to the coin score
    public void AddCoinScore(int value)
    {
        coinScore += value;

        // Update the coin score UI
        if (coinScoreText != null)
        {
            coinScoreText.text = "" + coinScore.ToString();
        }
    }

    public void AddGemScore(int value)
    {
        gemScore += value;

        // Update the gem score UI
        if (gemScoreText != null)
        {
            gemScoreText.text = "" + gemScore.ToString();
        }
    }
}
