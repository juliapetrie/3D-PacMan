using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class PlayVideoOnStart : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = false;
        videoPlayer.time = 0;
        videoPlayer.playbackSpeed = 1f;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;

        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        StartCoroutine(WarmUpAndPlay());
    }

    IEnumerator WarmUpAndPlay()
    {
        videoPlayer.Play(); // start decoding
        yield return new WaitForEndOfFrame(); // load first frame to prevent buffering issues
        videoPlayer.Pause(); // pause 
        videoPlayer.time = 0; // reset clean
        yield return new WaitForSeconds(0.2f); // delayed as it kept starting early
        videoPlayer.Play();
    }
}

