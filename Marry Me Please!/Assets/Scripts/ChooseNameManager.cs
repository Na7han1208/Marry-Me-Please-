using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseNameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text inputText;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button pauseButton;

    [SerializeField] private Material scrollBurnMaterial;
    public float burnDuration = 3f;

    void Awake(){
        scrollBurnMaterial.SetFloat("_Fade", 1f); // Set the initial value to 1
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            pressSubmitButton();
        }
    }

    public void pressSubmitButton()
    {
        if (inputText.text == "")
        {
            inputText.text = "John China";
        }

        //Save player name
        SaveData data = new SaveData();
        Debug.Log(inputText.text);
        data.playerName = inputText.text;
        SaveLoadManager.Instance.SaveGame(data);
        Debug.Log("Saved name: " + data.playerName);

        StartCoroutine(BeginGame());
        AudioManager.Instance.StopAll();
        AudioManager.Instance.Play("ScrollBurn");

        inputText.enabled = false;
    }

    IEnumerator BeginGame()
    {
        submitButton.enabled = false;
        pauseButton.enabled = false;

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
            //Debug.Log("DEBUG: " + currentValue);
        }

        scrollBurnMaterial.SetFloat("_Fade", endValue); // Just incase the previous while fucks up
        //Debug.Log("DEBUG: Scroll Burned");
    }
}
