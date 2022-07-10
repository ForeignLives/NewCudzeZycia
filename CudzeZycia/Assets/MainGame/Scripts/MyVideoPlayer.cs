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
    public AudioSource audioSource;
    public RenderTexture renderTexture;

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

        // https://forum.unity.com/threads/how-to-properly-fill-a-rendertexture-with-color-when-it-is-created.178410/
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = null;

        // videoPlayer.SetDirectAudioVolume(0, volume);
        audioSource.volume = volume;
        videoPlayer.Play();
        videoTexture.SetActive(true);


        Time.timeScale = 0; // pause game

        var delayms = Convert.ToInt32(Math.Ceiling(videoClip.length * 1000));

        while (delayms > 0)
        {
            await Task.Delay(100);
            delayms -= 100;
            if (skipAll) {
                videoPlayer.Stop();
                break;
            }
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
