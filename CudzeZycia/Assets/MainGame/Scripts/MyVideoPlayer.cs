using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

public class MyVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoTexture;

    private bool skipAll = false;

    void Start()
    {
        // domyœlnie ukryj video
        if (videoTexture.activeSelf){videoTexture.SetActive(false);}
    }

    // volume 1 = 100% 0 = mute
    public async Task PlayVideo(VideoClip videoClip, float volume=1)
    {
        videoPlayer.Stop();

        videoPlayer.clip = videoClip;
        videoPlayer.SetDirectAudioVolume(0, volume);
        videoTexture.SetActive(true);
        videoPlayer.Play();
        

        Time.timeScale = 0; // pause game

        var delayms = Convert.ToInt32(Math.Ceiling(videoClip.length * 1000));

        while (delayms > 0)
        {
            await Task.Delay(100);
            delayms -= 100;
            if (skipAll) { break; }
        }

        Time.timeScale = 1; // run game
        videoTexture.SetActive(false);
    }


    public void Debug_Skip_On()
    {
        skipAll = true;
    }

    public void Debug_Skip_Off()
    {
        skipAll = false;
    }

}
