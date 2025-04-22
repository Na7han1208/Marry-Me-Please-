using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("ChooseName");
    }

    public void OpenSettings(){
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit(1);
        //For the editor
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}