using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    [Header("Panel")]
    public GameObject settingsPanel;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private AudioManager audioManager;

    void Start()
    {
        // Optional: Initialize sliders to current volume levels
        masterSlider.value = AudioListener.volume;
        if (AudioManager.Instance != null)
        {
            bgmSlider.value = AudioManager.Instance.bgmSource.volume;
            sfxSlider.value = AudioManager.Instance.sfxSource.volume;
        }
        settingsPanel.SetActive(false);  
    }

    public void SetMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SetBGMVolume(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.bgmSource.volume = value;
    }

    public void SetSFXVolume(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.sfxSource.volume = value;
    }

    public void OpenSettings()
    {
        AudioManager.Instance.PlayButton();
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
         AudioManager.Instance.PlayButton();
        settingsPanel.SetActive(false);
    }

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
