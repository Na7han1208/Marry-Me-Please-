using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Master Volume")]
    [SerializeField] private Slider MasterVolumeSlider;
    [SerializeField] private TMP_Text MasterVolumeAmt;

    [Header("Dialogue Speed")]
    [SerializeField] private Slider DialogueSpeedSlider;
    [SerializeField] private TMP_Text DialogueSpeedAmt;

    [Header("Fullscreen")]
    [SerializeField] private GameObject fullscreenDumpling;
    private bool isFullscreen = true;

    void Awake()
    {
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        DialogueSpeedSlider.value = PlayerPrefs.GetFloat("DialogueSpeed", 1f);
        isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;

        fullscreenDumpling.SetActive(isFullscreen);
    }

    void Update()
    {
        MasterVolumeAmt.text = Math.Round(MasterVolumeSlider.value * 100) + "";
        DialogueSpeedAmt.text = Math.Round(DialogueSpeedSlider.value * 10) / 10 + "x";
    }

    public void ToggleFullScreen()
    {
        isFullscreen = !isFullscreen;
        if (isFullscreen)
        {
            fullscreenDumpling.SetActive(true);
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            fullscreenDumpling.SetActive(false);
        }
        Debug.Log(isFullscreen);
    }

    public void ReturnToMenu()
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterVolumeSlider.value);
        PlayerPrefs.SetFloat("DialogueSpeed", DialogueSpeedSlider.value);
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainMenu");
    }
}
