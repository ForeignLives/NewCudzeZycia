using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public Sound[] sounds;

	private Sound bgMusicPlayedNow = null;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject); 
			// uwaga audiomamager jest dziêki temu na wszystkich scenach i mo¿e odtwarzaæ wci¹¿ muzykê to dobrze ale
			// TODO: trzeba dorobiæ ¿eby PlayMusic anulowa³o poprzedni¹ muzykê czy coœ
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = s.mixerGroup;
		}

		// SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDestroy()
	{
		// SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public void Play(string sound)
	{
		var s = FindSound(sound);
		if(s == null) { return; }

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void PlayAsBackground(string sound)
    {
		var s = FindSound(sound);
		if (s == null) { Debug.LogError("PlayAsBackground sound not found");  return; }

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		if (bgMusicPlayedNow != s)
        {
            // its new song soo stop old sound and play new
            if (bgMusicPlayedNow != null)
				bgMusicPlayedNow.source.Stop(); // stop old
			
			bgMusicPlayedNow = s;
		}
		s.source.Play();
	}

	private Sound FindSound(string soundName)
    {
		Sound s = Array.Find(sounds, item => item.name == soundName);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return null;
		}
		return s;
	}

	public void StopBackground()
    {
		if (bgMusicPlayedNow != null)
		{
			bgMusicPlayedNow.source.Stop();
		}
	}

	public void StopBackground(string songName)
	{
		if (bgMusicPlayedNow != null)
		{
			if (bgMusicPlayedNow.name == songName) {
				bgMusicPlayedNow.source.Stop();
			}
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// wycisz / wy³¹cz / zresetuj muzykê w tle
		if (bgMusicPlayedNow != null)
        {
			bgMusicPlayedNow.source.Stop();
		}
	}
}
