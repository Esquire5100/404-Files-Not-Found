using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource AudioSource;
    public AudioClip backgroundMusic;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            AudioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);

            if (backgroundMusic != null)
                AudioSource.Play();
        }
        else
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
            AudioSource.clip = audioClip;
        }
        else if(AudioSource != null)
        {
            if(resetSong)
            {
                AudioSource.Stop();
            }
            AudioSource.Play();
        }
    }
}