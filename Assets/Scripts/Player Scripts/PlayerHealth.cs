using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // For TextMeshProUGUI
using UnityEngine.UI; // For Image and other UI components

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4; // Maximum health
    private int currentHealth;

    private Animator animator; // Reference to the Animator component
    private bool isDead = false; // Tracks if the player object is dead or not

    public GameObject heartContainer; // Object containing heart UI
    public Sprite fullHeart; // Sprite for a full heart
    public Sprite emptyHeart; // Sprite for an empty heart

    public AudioClip GameIsOver; // Ref to the game-over sound effect
    public AudioClip TookDamage; // Ref to the damage sound effect

    public TextMeshProUGUI youDiedText; // Reference to the "YOU DIED" text
    public GameObject gameOverPanel; // Reference to the Game Over UI panel

    public List<GameObject> enemiesToDisable; // List of enemies to disable

    private Image[] hearts;

    void Start()
    {
        currentHealth = maxHealth;

        // Get the Animator component attached to the player
        animator = GetComponent<Animator>();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (youDiedText != null)
        {
            youDiedText.enabled = false;
        }

        hearts = heartContainer.GetComponentsInChildren<Image>();
        UpdateHearts();

        // Ensure the "YOU DIED" text and Game Over panel are hidden initially
        if (youDiedText != null)
        {
            youDiedText.enabled = false;
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log($"Player Healed. Current health: {currentHealth}");

        // Update hearts in UI
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        if (!isDead) // Only take damage if the player is alive
        {
            currentHealth -= damage;

            AudioManager.instance.PlaySound(TookDamage);

            // Play the damaged animation
            if (animator != null)
            {
                animator.SetTrigger("Damaged");
            }

            // Update hearts in UI
            UpdateHearts();

            if (currentHealth <= 0)
            {
                Die();
            }
        }
            
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    private void Die()
    {
        if (isDead) return; // Prevent multiple death triggers
        isDead = true;

        foreach (GameObject enemy in enemiesToDisable)
        {
            if (enemy != null)
            {
                enemy.SetActive(false);
                Debug.Log($"Disabled enemy: {enemy.name}");
            }
        }

        AudioManager.instance.PlaySound(GameIsOver);

        // Trigger the dying animation
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Disable player movement or actions
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Stop the player
        GetComponent<PlayerMovement>().enabled = false; // Disable movement script

        Debug.Log("Player is Dead!");

        // Show the "YOU DIED" text
        if (youDiedText != null)
        {
            youDiedText.enabled = true;
        }

        // Disable all enemies in the list
        DisableEnemies();

        // Show the Game Over panel after a delay
        Invoke(nameof(ShowGameOverPanel), 2f);
    }

    private void DisableEnemies()
    {
        foreach (var enemy in enemiesToDisable)
        {
            if (enemy != null)
            {
                enemy.SetActive(false); // Disable each enemy
            }
        }
    }

    private void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("InGame");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
