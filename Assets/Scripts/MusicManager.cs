using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource audioSource;
    public AudioClip backgroundMusic;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            if (backgroundMusic != null)
            {
                audioSource.clip = backgroundMusic;
                audioSource.volume = audioSource.volume > 0 ? audioSource.volume : 1f;
                audioSource.pitch = audioSource.pitch != 0 ? audioSource.pitch : 1f;
                audioSource.Play();
            }
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if(backgroundMusic != null)
        {
            PlayBackgroundMusic(false, backgroundMusic);
        }
    }
    
    public void PlayBackgroundMusic(bool resetSong, AudioClip audioClip = null)
    {
        if(audioClip != null)
        {
            audioSource.clip = audioClip;
        }
        else if(audioSource != null)
        {
            if(resetSong)
            {
                audioSource.Stop();
            }
            audioSource.Play();
        }
    }

    public void PausedBackgroundMusic()
    {
        audioSource.Pause();
    }
}