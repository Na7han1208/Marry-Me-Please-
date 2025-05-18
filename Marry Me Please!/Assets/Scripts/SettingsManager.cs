using System;
using TMPro;
using UnityEditor.Rendering.Fullscreen.ShaderGraph;
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
        if (SaveLoadManager.Instance.SaveExists())
        {
            SaveData data = SaveLoadManager.Instance.LoadGame();
            MasterVolumeSlider.value = data.masterVolume;
            DialogueSpeedSlider.value = data.dialogueSpeed;
            isFullscreen = data.fullscreen;
            if (data.fullscreen)
            {
                fullscreenDumpling.SetActive(true); ;
            }
            else
            {
                fullscreenDumpling.SetActive(false);
            }
        }
        else
        {
            MasterVolumeSlider.value = 1f;
            DialogueSpeedSlider.value = 1f;
        }
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
        //Save Changes
        SaveData data = SaveLoadManager.Instance.LoadGame();
        data.masterVolume = MasterVolumeSlider.value;
        data.dialogueSpeed = DialogueSpeedSlider.value;
        data.fullscreen = isFullscreen;
        SaveLoadManager.Instance.SaveGame(data);

        SceneManager.LoadScene("MainMenu");
    }
}
