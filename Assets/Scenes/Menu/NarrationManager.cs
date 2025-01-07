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
        audioSource = GetComponent<AudioSource>();

        // Subscribe to scene-loaded events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is missing. Please add one to the NarrationManager GameObject.");
            return;
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        foreach (SceneNarration sn in sceneNarrations)
        {
            if (sn.sceneName == scene.name && sn.narrationClip != null)
            {
                StartCoroutine(PlayNarrationWithDelay(sn.narrationClip, 0.5f)); // 1-second delay
                break;
            }
        }
    }

    private IEnumerator PlayNarrationWithDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        audioSource.clip = clip;
        audioSource.Play();
        Debug.Log("Playing narration after delay: " + clip.name);
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
