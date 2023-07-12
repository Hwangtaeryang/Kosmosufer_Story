using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerCtrl : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClip;

    bool firstEnd;
    int count = 0;

    void Start()
    {
        videoPlayer.loopPointReached += CheckOver;  //���� �������� üũ
    }


    void CheckOver(VideoPlayer vi)
    {
        if(count.Equals(0))
        {
            count += 1;
            print("Video Is Over");
            firstEnd = true;
            //videoPlayer.Stop();
        }
    }

    private void Update()
    {
        if (firstEnd.Equals(true))
        {
            firstEnd = false;
            videoPlayer.clip = videoClip[1];
            videoPlayer.isLooping = true;
            videoPlayer.Play();
        }
    }
}
