using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class EndScreenManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject endingText;
    [SerializeField] private GameObject[] characters;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject returnButton;

    [Header("End Backgrounds")]
    [SerializeField] private Sprite mingEndingBG;
    [SerializeField] private Sprite theoEndingBG;
    [SerializeField] private Sprite zihanEndingBG;
    [SerializeField] private Sprite fenEndingBG;
    [SerializeField] private Sprite yilinEndingBG;
    [SerializeField] private Sprite yukiEndingBG;
    [SerializeField] private Sprite jinhuiEndingBG;
    [SerializeField] private Sprite noBitchesEndingBG;

    [Header("End Text")]
    [TextArea] [SerializeField] private String mingEndingTxt;
    [TextArea] [SerializeField] private String theoEndingTxt;
    [TextArea] [SerializeField] private String zihanEndingTxt;
    [TextArea] [SerializeField] private String fenEndingTxt;
    [TextArea] [SerializeField] private String yilinEndingTxt;
    [TextArea] [SerializeField] private String yukiEndingTxt;
    [TextArea] [SerializeField] private String jinhuiEndingTxt;
    [TextArea] [SerializeField] private String noBitchesEndingTxt;

    void Start()
    {
        endingText.GetComponent<TMP_Text>().text = "";
        returnButton.SetActive(false);

        SaveData data = SaveLoadManager.Instance.LoadGame();

        if (data.mingAffinity > 50) characters[0].SetActive(true);
        if (data.theodoreAffinity > 50) characters[1].SetActive(true);
        if (data.zihanAffinity > 50) characters[2].SetActive(true);
        if (data.fenAffinity > 50) characters[3].SetActive(true);
        if (data.yilinAffinity > 50) characters[4].SetActive(true);
        if (data.yukiAffinity > 50) characters[5].SetActive(true);
        if (data.jinhuiAffinity > 50) characters[6].SetActive(true);

        if (playerGetsNoBitches(characters))
        {
            NoBitchesEnding();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private Boolean playerGetsNoBitches(GameObject[] characters)
    {
        foreach (GameObject character in characters)
        {
            if (character.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    public void MingEnding()
    {
        PlayerPrefs.SetInt("MingUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = mingEndingBG;
        endingText.GetComponent<TMP_Text>().text = mingEndingTxt;
        StartCoroutine(PlayEnding());
    }

    public void TheoEnding()
    {
        PlayerPrefs.SetInt("TheoUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = theoEndingBG;
        endingText.GetComponent<TMP_Text>().text = theoEndingTxt;
        StartCoroutine(PlayEnding());
    }

    public void ZihanEnding()
    {
        PlayerPrefs.SetInt("ZihanUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = zihanEndingBG;
        endingText.GetComponent<TMP_Text>().text = zihanEndingTxt;
        StartCoroutine(PlayEnding());
    }

    public void FenEnding()
    {
        PlayerPrefs.SetInt("FenUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = fenEndingBG;
        endingText.GetComponent<TMP_Text>().text = fenEndingTxt;
        StartCoroutine(PlayEnding());
    }

    public void YilinEnding()
    {
        PlayerPrefs.SetInt("YilinUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = yilinEndingBG;
        endingText.GetComponent<TMP_Text>().text = yilinEndingTxt;
        StartCoroutine(PlayEnding());
    }

    public void YukiEnding()
    {
        PlayerPrefs.SetInt("YukiUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = yukiEndingBG;
        endingText.GetComponent<TMP_Text>().text = yukiEndingTxt;
        StartCoroutine(PlayEnding());
    }

    public void JinhuiEnding()
    {
        PlayerPrefs.SetInt("MingUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = jinhuiEndingBG;
        endingText.GetComponent<TMP_Text>().text = jinhuiEndingTxt;
        StartCoroutine(PlayEnding());
    }

    public void NoBitchesEnding()
    {
        PlayerPrefs.SetInt("JinhuiUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = noBitchesEndingBG;
        endingText.GetComponent<TMP_Text>().text = noBitchesEndingTxt;
        StartCoroutine(PlayEnding());
    }

    IEnumerator PlayEnding()
    {
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        returnButton.SetActive(true);
        float scrollSpeed = 60f;
        float endOffset = 100f;

        RectTransform rt = GetComponent<RectTransform>();

        // Start position: below the screen
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -Screen.height);

        // Calculate target position: above the screen
        float targetY = Screen.height + rt.rect.height + endOffset;

        // Scroll upward
        while (rt.anchoredPosition.y < targetY)
        {
            rt.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
