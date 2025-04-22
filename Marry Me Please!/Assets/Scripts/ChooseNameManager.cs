using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseNameManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button pauseButton;
    public static string playerName;

    public void pressSubmitButton(){
        StartCoroutine("BeginGame");
    }

    IEnumerator BeginGame(){
        playerName = inputField.text;

        submitButton.enabled = false;
        pauseButton.enabled = false;

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Main");
    }

    public void pressPauseButton(){
        SceneManager.LoadScene("MainMenu");
    }
}
