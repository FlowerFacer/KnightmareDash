using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class NarrationManager : MonoBehaviour
{
    [System.Serializable]
    public class SceneNarration
    {
        public string sceneName;  // The name of the scene
        public AudioClip narrationClip;  // The narration audio clip
    }

    public SceneNarration[] sceneNarrations; // Array of scene-narration pairs
    private AudioSource audioSource;
    private static NarrationManager instance;

    private void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    private void Start()
    {
        // Ensure AudioSource is attached
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is missing. Please add one to the NarrationManager GameObject.");
        }

        // Subscribe to scene-loaded events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is missing. Cannot play narration.");
            return;
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Dynamically load the audio clip from the Resources folder
        string clipName = $"{scene.name}";
        AudioClip clip = Resources.Load<AudioClip>($"Narrations/{clipName}");

        if (clip != null)
        {
            StartCoroutine(PlayNarrationWithDelay(clip, 0.5f));
            Debug.Log($"Playing narration for scene: {scene.name}");
        }
        else
        {
            Debug.LogWarning($"No narration found for scene: {scene.name}");
        }
    }

    private IEnumerator PlayNarrationWithDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        if (audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
            Debug.Log("Playing narration: " + clip.name);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
