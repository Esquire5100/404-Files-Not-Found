using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource audioSource;
    public AudioClip backgroundMusic; // Main Menu
    public AudioClip secondBackgroundMusic; // In-game

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            SceneManager.sceneLoaded += OnSceneLoaded; // Hook into scene load

            PlayMusicForScene(SceneManager.GetActiveScene().name); // Play music for the current scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to prevent memory leaks
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    void PlayMusicForScene(string sceneName)
    {
        if (sceneName == "Main Menu")
        {
            SwitchMusic(backgroundMusic);
        }
        else
        {
            SwitchMusic(secondBackgroundMusic);
        }
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