using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource audioSource;

    public AudioClip backgroundMusic;        // For Main Menu, Settings, Credits, Files
    public AudioClip secondBackgroundMusic;  // For in-game scenes

    public AudioClip victoryYippie;          // Plays once at the start of victory
    public AudioClip victoryCheerLoop;       // Loops after yippie

    private Coroutine victoryCoroutine;      // To prevent overlapping coroutines

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            // Subscribe to scene loaded callback
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Play music for the current scene
            PlayMusicForScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    void PlayMusicForScene(string sceneName)
    {
        // Stop any running coroutine if switching scenes
        if (victoryCoroutine != null)
        {
            StopCoroutine(victoryCoroutine);
            victoryCoroutine = null;
        }

        if (sceneName == "Victory")
        {
            victoryCoroutine = StartCoroutine(PlayVictorySequence());
        }
        else if (sceneName == "Main Menu" ||
                 sceneName == "Settings" ||
                 sceneName == "Credits" ||
                 sceneName == "Files")
        {
            SwitchMusic(backgroundMusic);
        }
        else
        {
            SwitchMusic(secondBackgroundMusic);
        }
    }

    IEnumerator PlayVictorySequence()
    {
        // Stop any currently playing music
        audioSource.Stop();
        audioSource.loop = false;

        // Play one-shot Yippie
        audioSource.PlayOneShot(victoryYippie);

        // Wait for it to finish
        yield return new WaitForSeconds(victoryYippie.length);

        // Now play the looping cheering audio
        audioSource.clip = victoryCheerLoop;
        audioSource.loop = true;
        audioSource.Play();
    }

    void SwitchMusic(AudioClip clip)
    {
        if (audioSource.clip == clip)
            return; // Don't restart if it's already playing this clip

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PauseBackgroundMusic()
    {
        audioSource.Pause();
    }

    public void ResumeBackgroundMusic()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}