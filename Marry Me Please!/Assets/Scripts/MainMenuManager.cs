using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Unity.VisualScripting;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scrolls")]
    public GameObject scroll1;
    public GameObject scroll2;
    public GameObject scroll3;
    public GameObject scroll4;

    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        if (!AudioManager.Instance.isPlaying("MainMenuMusic"))
        {
            AudioManager.Instance.Play("MainMenuMusic");
        }
            
        //AudioManager.Instance.Play("MainMenuMusic");
        try
        {
            scroll1.GetComponent<Animator>().enabled = false;
            scroll2.GetComponent<Animator>().enabled = false;
            scroll3.GetComponent<Animator>().enabled = false;
            scroll4.GetComponent<Animator>().enabled = false;

            scroll1.GetComponentInChildren<TMP_Text>().enabled = false;
            scroll2.GetComponentInChildren<TMP_Text>().enabled = false;
            scroll3.GetComponentInChildren<TMP_Text>().enabled = false;
            scroll4.GetComponentInChildren<TMP_Text>().enabled = false;
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }

        StartCoroutine(ScrollsUnfurl(0.3f));
    }

    public void NewGame()
    {
        SaveLoadManager.Instance.DeleteSave();
        SceneManager.LoadScene("ChooseName");
    }

    public void ResumeGame()
    {
        if (SaveLoadManager.Instance.SaveExists())
        {
            SceneManager.LoadScene("Main");
            AudioManager.Instance.Stop("MainMenuMusic");
        }
        else
        {
            Debug.LogWarning("No saved game found.");
        }
    }

    public void BonusContent()
    {
        SceneManager.LoadScene("BonusContent");
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

    private IEnumerator ScrollsUnfurl(float delay)
    {
        AudioManager audio = new AudioManager();
        scroll1.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(delay);
        scroll1.GetComponentInChildren<TMP_Text>().enabled = true;
        AudioManager.Instance.Play("ScrollUnfurl");

        scroll2.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(delay);
        scroll2.GetComponentInChildren<TMP_Text>().enabled = true;
        AudioManager.Instance.Play("ScrollUnfurl");

        scroll3.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(delay);
        scroll3.GetComponentInChildren<TMP_Text>().enabled = true;
        AudioManager.Instance.Play("ScrollUnfurl");

        scroll4.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(delay);
        scroll4.GetComponentInChildren<TMP_Text>().enabled = true;
        AudioManager.Instance.Play("ScrollUnfurl");
    }
}
