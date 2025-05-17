using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void NewGame()
    {
        SaveLoadManager.Instance.DeleteSave();
        SceneManager.LoadScene("ChooseName"); 
    }

    public void ResumeGame()
    {
        if (SaveLoadManager.Instance.SaveExists())
        {
            SaveData data = SaveLoadManager.Instance.LoadGame();
            if (data != null && !string.IsNullOrEmpty(data.sceneName))
            {
                SceneManager.LoadScene(data.sceneName);
            }
            else
            {
                Debug.LogWarning("Save data corrupted or empty scene.");
            }
        }
        else
        {
            Debug.LogWarning("No saved game found.");
        }
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
