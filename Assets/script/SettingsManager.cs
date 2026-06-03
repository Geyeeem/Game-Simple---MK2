using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioMixer masterMixer;

    [Header("UI Panels")]
    public GameObject settingPanel;

    public void SetVolume(float volume)
    {
        masterMixer.SetFloat("MyVolume", Mathf.Log10(volume) * 20);
    }

    public void OpenSettings()
    {
        settingPanel.SetActive(true);
        Time.timeScale = 0f; // Game pause
    }

    public void CloseSettings()
    {
        settingPanel.SetActive(false);
        Time.timeScale = 1f; // Game lanjut
    }
}