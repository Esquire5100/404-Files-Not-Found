using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance { get; private set; }

    private static AudioSource audioSource;
    private static SoundEffectLibrary soundEffectLibrary;
    //[SerializeField] private Slider sfxSlider;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            Debug.LogError("SoundEffectManager needs an AudioSource component!");

        var lib = GetComponent<SoundEffectLibrary>();
        if (lib == null)
            Debug.LogError("SoundEffectLibrary component missing!");
        soundEffectLibrary = lib;
    }

    public static void Play(string soundName)
    {
        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName);
        if(audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
    // Start is called before the first frame update
    /*void Start()
    {
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); }); 
    }*/

    public static void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    /*public void OnValueChanged()
    {
        SetVolume(sfxSlider.value); 
    }*/

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
