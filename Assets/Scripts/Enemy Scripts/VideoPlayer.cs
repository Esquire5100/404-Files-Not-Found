using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoTransition : MonoBehaviour
{
    VideoPlayer video;

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
        SceneManager.LoadScene("Game Over");
    }

    public void PlayVideo()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += OnVideoEnd; // Event to handle when the video is finished
    }
}