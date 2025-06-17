using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject endingText;
    [SerializeField] private GameObject[] characters;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject returnButton;
    [SerializeField] private RectTransform scrollContent;

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
    [TextArea][SerializeField] private String mingEndingTxt;
    [TextArea][SerializeField] private String theoEndingTxt;
    [TextArea][SerializeField] private String zihanEndingTxt;
    [TextArea][SerializeField] private String fenEndingTxt;
    [TextArea][SerializeField] private String yilinEndingTxt;
    [TextArea][SerializeField] private String yukiEndingTxt;
    [TextArea][SerializeField] private String jinhuiEndingTxt;
    [TextArea][SerializeField] private String noBitchesEndingTxt;

    void Start()
    {
        endingText.GetComponent<TMP_Text>().text = "";
        returnButton.SetActive(false);

        SaveData data = SaveLoadManager.Instance.LoadGame();

        if (data.mingAffinity > 20)
        {
            characters[0].SetActive(true);
        }

        if (data.theodoreAffinity > 20)
        {
            characters[1].SetActive(true);
        }

        if (data.zihanAffinity > 20)
        {
            characters[2].SetActive(true);
        }

        if (data.fenAffinity > 20)
        {
            characters[3].SetActive(true);
        }

        if (data.yilinAffinity > 20)
        {
            characters[4].SetActive(true);
        }

        if (data.yukiAffinity > 20)
        {
            characters[5].SetActive(true);
        }

        if (data.jinhuiAffinity > 20)
        {
            characters[6].SetActive(true);
        }

        if (playerGetsNoBitches(characters))
        {
            NoBitchesEnding();
        }
    }

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
                return false; 
            }
        }
        return true;
    }

    public void setAllInactive()
    {
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }
    }

    public void MingEnding()
    {
        setAllInactive();
        PlayerPrefs.SetInt("MingUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = mingEndingBG;
        endingText.GetComponent<TMP_Text>().text = mingEndingTxt;
        StartCoroutine(PlayEnding());
    }

    public void TheoEnding()
    {
        setAllInactive();
        PlayerPrefs.SetInt("TheoUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = theoEndingBG;
        endingText.GetComponent<TMP_Text>().text = theoEndingTxt;
        StartCoroutine(PlayEnding());
    }

    public void ZihanEnding()
    {
        setAllInactive();
        PlayerPrefs.SetInt("ZihanUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = zihanEndingBG;
        endingText.GetComponent<TMP_Text>().text = zihanEndingTxt;
        StartCoroutine(PlayEnding());
    }

    public void FenEnding()
    {
        setAllInactive();
        PlayerPrefs.SetInt("FenUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = fenEndingBG;
        endingText.GetComponent<TMP_Text>().text = fenEndingTxt;
        StartCoroutine(PlayEnding());
    }

    public void YilinEnding()
    {
        setAllInactive();
        PlayerPrefs.SetInt("YilinUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = yilinEndingBG;
        endingText.GetComponent<TMP_Text>().text = yilinEndingTxt;
        StartCoroutine(PlayEnding());
    }

    public void YukiEnding()
    {
        setAllInactive();
        PlayerPrefs.SetInt("YukiUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = yukiEndingBG;
        endingText.GetComponent<TMP_Text>().text = yukiEndingTxt;
        StartCoroutine(PlayEnding());
    }

    public void JinhuiEnding()
    {
        setAllInactive();
        PlayerPrefs.SetInt("JinhuiUnlocked", 1);
        background.GetComponent<UnityEngine.UI.Image>().sprite = jinhuiEndingBG;
        endingText.GetComponent<TMP_Text>().text = jinhuiEndingTxt;
        StartCoroutine(PlayEnding());
    }

    public void NoBitchesEnding()
    {
        setAllInactive();
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

        float scrollSpeed = 45f;
        float startOffset = -500f;
        float endOffset = 100000f;

        scrollContent.anchoredPosition = new Vector2(
            scrollContent.anchoredPosition.x,
            -Screen.height - startOffset
        );

        float targetY = Screen.height + scrollContent.rect.height + endOffset;

        while (scrollContent.anchoredPosition.y < targetY)
        {
            scrollContent.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
