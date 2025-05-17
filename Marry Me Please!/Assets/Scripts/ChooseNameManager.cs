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
    [SerializeField] private TMP_Text submitButtonText;
    [SerializeField] private Button pauseButton;
    public static string playerName;

    [SerializeField] private Material scrollBurnMaterial;
    public float burnDuration = 3f;

    void Awake(){
        scrollBurnMaterial.SetFloat("_Fade", 1f); // Set the initial value to 1
    }

    public void pressSubmitButton()
    {
        StartCoroutine("BeginGame");
    }

    IEnumerator BeginGame()
    {
        playerName = inputField.text;

        submitButton.enabled = false;
        pauseButton.enabled = false;

        submitButtonText.text = "";
        inputField.text = " ";

        StartCoroutine(ScrollBurnEffect());
        yield return new WaitForSeconds(burnDuration);
        // Load the next scene after the burn effect
        SceneManager.LoadScene("Main");
    }

    public void pressPauseButton(){
        SceneManager.LoadScene("MainMenu");
    }



    IEnumerator ScrollBurnEffect()
    {
        float timeElapsed = 0f;
        float startValue = 1f;
        float endValue = 0f;

        while (timeElapsed < burnDuration)
        {
            float currentValue = Mathf.Lerp(startValue, endValue, timeElapsed / burnDuration); // Moves between startValuie and endValue over time
            scrollBurnMaterial.SetFloat("_Fade", currentValue);
            timeElapsed += Time.deltaTime;
            yield return null;
            Debug.Log("DEBUG: " + currentValue);
        }

        scrollBurnMaterial.SetFloat("_Fade", endValue); // Just incase the previous while fucks up
        Debug.Log("DEBUG: Scroll Burned");
    }
}
