using UnityEngine;
using UnityEngine.Video;

public class PlayVideoOnStart : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.playOnAwake = false;  
        videoPlayer.time = 0;             
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();            // Begin vid prep
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        vp.time = 0;   // video at start
        vp.Play();     // vid plays at normal speed
    }
}
