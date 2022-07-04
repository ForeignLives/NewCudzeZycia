using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSound : MonoBehaviour
{
    public float minPitch = 0.9f, maxPitch = 1.1f, minVolume = 0.1f, maxVolume = 0.2f;
    private AudioSource Player;
    void Start()
    {
        Player = GetComponent<AudioSource>();
    }

    void StepFootTurnOn()
    {
        Player.pitch = Random.Range(minPitch, maxPitch);
        Player.volume = Random.Range(minVolume, maxVolume);
        Player.Play();
    }
}
