using UnityEngine;


public class PlayMusic : MonoBehaviour
{
    [StringInList(typeof(AllSongNames), "GetAllSongNames")]
    public string songName;

    public bool isAmbientOrBackgroundSong = true;

    void OnEnable()
    {
        Debug.Log("play music");
        if (isAmbientOrBackgroundSong)
        {
            FindObjectOfType<AudioManager>().PlayAsBackground(songName);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play(songName);
        }
    }

    private void OnDisable()
    {
        Debug.Log("stop music");
        FindObjectOfType<AudioManager>().StopBackground();
    }
}
