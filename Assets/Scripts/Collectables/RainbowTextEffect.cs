using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RainbowTextEffect : MonoBehaviour
{
    public TextMeshProUGUI text; // ref to the TextMeshProUGUI component
    public float speed = 0.5f; // Speed of rainbow fx

    private string currentText = ""; // Store the current text

    void Start()
    {
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        text.enabled = false; // Initially hide the text
    }

    private void Update()
    {
        if (text == null || string.IsNullOrEmpty(currentText)) return;

        // Generates a rainbow effect using sine waves
        string rainbowText = "";

        for (int i = 0; i < currentText.Length; i++)
        {
            // Calculate the color for each character
            float hue = Mathf.PingPong(Time.time * speed + i * 0.1f, 1f);
            Color color = Color.HSVToRGB(hue, 1f, 1f);
            string hexColor = ColorUtility.ToHtmlStringRGB(color);

            // Apply the color to the character using rich text
            rainbowText += $"<color=#{hexColor}>{currentText[i]}</color>";
        }

        // Update the text
        text.text = rainbowText;
    }

    public void ShowText(string message)
    {
        currentText = message; // Update the current text content
        text.enabled = true;   // Make the text visible
    }
}
