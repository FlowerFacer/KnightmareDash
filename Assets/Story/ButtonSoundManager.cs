using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    public AudioClip buttonClickSound; // ref to the button sound FX
    private AudioSource audioSource;

    void Start()
    {
        // Get or add an AudioSource component to the Game Object
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Optional: configure the AudioSource settings
        audioSource.playOnAwake = false;
    }

    public void PlayButtonSound()
    {
        if (buttonClickSound == null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
