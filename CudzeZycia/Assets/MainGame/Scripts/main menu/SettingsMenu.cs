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
    private List<string> resolutionsWhitelist = new List<string>{
       "426x240",
       "640x360",
       "854x480",
       "1280x720",
       "1920x1080",
       "2560x1440",
       "3840x2160",
    };

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
            // TODO: remove weird resolutions from there so players cant choice freezer screen resolution lol - @GorlikItsMe
            var res = resolutions[i];
            if (resolutionsWhitelist.Contains(res.width + "x" + res.height))
            {
                options.Add(res.width + " x " + res.height + " ("+res.refreshRate+"Hz)");
                if (Screen.currentResolution.Equals(res))
                {
                    currentResolutionIndex = i;
                }
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
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
