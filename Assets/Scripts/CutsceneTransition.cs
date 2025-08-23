using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UIElements;

public class CutsceneTransition : MonoBehaviour
{
   VideoPlayer video;
   public string sceneToLoad;
   //for crossfade transitions
   public Animator transition;
   public float transitionTime = 1f;

   void Awake()
   {
        //PlayVideo();
        //video = GetComponent<VideoPlayer>();
        //video.Play();
        //video.loopPointReached += OnVideoEnd; // Event to handle when the video is finished
   }

   void OnVideoEnd(UnityEngine.Video.VideoPlayer vp)
   {
        // Replace '1' with the build index of your next scene
        //SceneManager.LoadScene("Victory");
        //transition = GameObject.Find("Fading Image").GetComponent<Animator>();
        transition.SetTrigger("Start");            //transition = GameObject.Find("FadingImage").GetComponent<Animator>();
        StartCoroutine(levelLoad());
   }

   public void PlayVideo()
   {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += OnVideoEnd; // Event to handle when the video is finished
   }

   IEnumerator levelLoad()
    {
            //transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(sceneToLoad);
    }
}
