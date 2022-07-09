using UnityEngine;


public class PlayMusic : MonoBehaviour
{
    [StringInList(typeof(AllSongNames), "GetAllSongNames")]
    public string songName;

    public bool isAmbientOrBackgroundSong = true;

    void OnEnable()
    {
        Debug.Log("play music ("+ songName +")");
        var audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            if (isAmbientOrBackgroundSong)
            {
                audioManager.PlayAsBackground(songName);
            }
            else
            {
                audioManager.Play(songName);
            }
        }
        else
        {
            Debug.LogError("AudioManager not fonud (pewnie dlatego ¿e debugujesz)");
        }

    }

    private void OnDisable()
    {
        Debug.Log("stop music (" + songName + ")");
        FindObjectOfType<AudioManager>().StopBackground(songName);
    }
}
