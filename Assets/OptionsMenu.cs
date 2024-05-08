using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class OptionsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle vsyncToggle;
    public Toggle fullscreenToggle;
    public GameObject back;
    public GameObject res;



    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(option));

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        LoadSettings(currentResolutionIndex);

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        res.GetComponent<AudioSource>().Play();
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        vsyncToggle.GetComponent<AudioSource>().Play();
        Screen.fullScreen = isFullscreen;
    }

    public void SetVSync(bool isVSync)
    {
        vsyncToggle.GetComponent<AudioSource>().Play();
        QualitySettings.vSyncCount = isVSync ? 1 : 0;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);

        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);

        PlayerPrefs.SetInt("VSync", vsyncToggle.isOn ? 1 : 0);

        PlayerPrefs.Save();

        Debug.Log("Settings have been saved!");
    }

    private void LoadSettings(int defaultResolutionIndex)
    {
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", defaultResolutionIndex);
        if (savedResolutionIndex >= 0 && savedResolutionIndex < resolutions.Length)
        {
            Resolution resolution = resolutions[savedResolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            resolutionDropdown.value = savedResolutionIndex;
        }

        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        Screen.fullScreen = isFullscreen;
        fullscreenToggle.isOn = isFullscreen;

        bool isVSync = PlayerPrefs.GetInt("VSync", 0) == 1;
        QualitySettings.vSyncCount = isVSync ? 1 : 0;
        vsyncToggle.isOn = isVSync;
    }

    public void Back()
    {
        StartCoroutine(PlaySoundAndBack(back, "MainMenu"));
    }

    IEnumerator PlaySoundAndBack(GameObject obj, string sceneName)
    {
        AudioSource audio = obj.GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length); 
        SceneManager.LoadScene(sceneName);
    } 


}
