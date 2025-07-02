using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Label of Audio Sources
    [Header("-----------Audio Source-----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    //Add Audio clips to each varible
    [Header("-----------Audio Clip-----------")]
    public AudioClip LevelMusic;
    public AudioClip MainMenu;
    public AudioClip LoseGameSFX;
    public AudioClip WhooshSFX;
    public AudioClip FlashbangSFX;
    public AudioClip DistractionSFX;           //Pen Dropping
    public AudioClip AlertMusic;
    public AudioClip LiftSFX;
    public AudioClip ClickSFX;
    public AudioClip WalkSFX;

    //Music to start playing when the level starts
    private void Start()
    {
        musicSource.clip = LevelMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)                 //public to access it from other scripts
    {                                                   //Takes Audio Clip as a parameter
        SFXSource.PlayOneShot(clip);                    //Able to play the sound effect we want
    }
    
    //Add following to script to add SFX Sound effect

    //(Varible) : AudioManager audioManager;

    /*Script:
     * private void Awake()
     * {
     *      audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
     * }
     *
     * XXX
     * {
     *      XXX
     *      {
     *          audioManager.PlayerSFX(audioManager.XX);
     *          XX
     *      }
     *  }
     */

}                                                       
