using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class OptionsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle vsyncToggle;
    public Toggle fullscreenToggle;
    // public Slider masterVolumeSlider;
    // public Slider musicVolumeSlider;
    // public Slider sfxVolumeSlider;
    // public Slider brightnessSlider;
    // public Slider contrastSlider;
    // public Slider colorationSlider;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            Debug.Log(resolutions[i]);
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(option));

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        //LoadSettings();
    }

    public void SetResolution(int i)
    {
        // Debug.Log(resolutionDropdown.value);
        // Debug.Log(resolutions[resolutionDropdown.value]);

        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        Debug.Log("Current Resolution: " + screenWidth + "x" + screenHeight);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetVSync(bool isVSync)
    {
        QualitySettings.vSyncCount = isVSync ? 1 : 0;
    }

    /*
    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        // Assuming music source is tagged as Music
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        // Assuming SFX sources are tagged as SFX
        foreach (var source in GameObject.FindGameObjectsWithTag("SFX"))
        {
            source.GetComponent<AudioSource>().volume = volume;
        }
        PlayerPrefs.SetFloat("SFXVolumePreference", volume);
    }

    public void SetBrightness(float brightness)
    {
        // Implement brightness change logic here
        PlayerPrefs.SetFloat("BrightnessPreference", brightness);
    }

    public void SetContrast(float contrast)
    {
        // Implement contrast change logic here
        PlayerPrefs.SetFloat("ContrastPreference", contrast);
    }

    public void SetColoration(float coloration)
    {
        // Implement coloration change logic here
        PlayerPrefs.SetFloat("ColorationPreference", coloration);
    }
    */

    public void back()
    {
        SceneManager.LoadScene("MainMenu");

    }

}
