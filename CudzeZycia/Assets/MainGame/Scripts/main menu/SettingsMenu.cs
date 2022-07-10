using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;

    private Resolution[] resolutions;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        // set options in resolutionDropdown and set default resolution
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        var options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            var res = resolutions[i];
            options.Add(res.width + " x " + res.height + " ("+res.refreshRate+"Hz)");
            if (Screen.currentResolution.Equals(res))
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        // set default quality in dropdown
        qualityDropdown.value = QualitySettings.GetQualityLevel();

        // set default fullscrean
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityId)
    {
        QualitySettings.SetQualityLevel(qualityId);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resId)
    {
        var res = resolutions[resId];
        Debug.Log("Ustaw rozdzielczoœæ na " + res.width + " x " + res.height + "  resId: " + resId);
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
