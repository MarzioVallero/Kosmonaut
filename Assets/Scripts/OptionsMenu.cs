using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;

    bool enableVibration;
    Resolution[] resolutions;
    GameObject indestructable;
    SettingsManager settingsManager;

    private void Start()
    {
        //Prova variabile locale invece dello script esterno (forse lo script non muore nel cambio scena)
        Debug.Log("start");

        SceneManager.activeSceneChanged += ImportSettings;

        indestructable = GameObject.Find("IndestructableData");
        settingsManager = indestructable.GetComponent<SettingsManager>();

        GameObject vibrationToggle = GameObject.Find("VibrationToggle");
        Toggle toggle = vibrationToggle.GetComponent<Toggle>();

        toggle.isOn = settingsManager.enableVibration;

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void ImportSettings(Scene current, Scene next)
    {
        Debug.Log("importsettings "+settingsManager.enableVibration);
        
        SetVibration(settingsManager.enableVibration);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetVibration(bool enableVibration)
    {
        try
        {
            GameObject soyuz = GameObject.Find("Soyuz");
            JoystickController joystickController = soyuz.GetComponent<JoystickController>();

            joystickController.SetVibration(enableVibration);
        }
        catch
        {
            settingsManager.enableVibration = enableVibration;
        }
    }

    public void SetController(int controllerIndex)
    {
        Debug.Log(controllerIndex);
        if (settingsManager.controllerType == "XBOX")
            settingsManager.controllerType = "PlayStation";
        else if (settingsManager.controllerType == "PlayStation")
            settingsManager.controllerType = "XBOX";
    }
}

