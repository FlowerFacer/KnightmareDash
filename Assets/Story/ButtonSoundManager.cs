using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    public AudioClip buttonClickSound; // The sound to play when a button is pressed
    public float volume = 0.35f; // Adjustable volume for the button sound


    private AudioSource audioSource;

    void Start()
    {
        // Ensure there is an AudioSource attached to this object
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is missing on ButtonSoundManager. Please add one!");
        }
    }

    public void PlayButtonSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound, volume); // Use adjustable volume
        }
        else
        {
            if (audioSource == null)
            {
                Debug.LogError("AudioSource is not assigned.");
            }
            if (buttonClickSound == null)
            {
                Debug.LogError("Button click sound is not assigned.");
            }
        }
    }
}
