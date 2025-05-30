using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_Text affinityTxt;
    public GameObject[] characters;

    void Start()
    {
        SaveData data = SaveLoadManager.Instance.LoadGame();

        if (data.mingAffinity > 50) characters[0].SetActive(false);
        if (data.theodoreAffinity > 50) characters[1].SetActive(false);
        if (data.zihanAffinity > 50) characters[2].SetActive(false);
        if (data.fenAffinity > 50) characters[3].SetActive(false);
        if (data.yilinAffinity > 50) characters[4].SetActive(false);
        if (data.yukiAffinity > 50) characters[5].SetActive(false);
        if (data.jinhuiAffinity > 50) characters[6].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
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

    void mingEnding()
    {

    }

    void theoEnding()
    {

    }

    void zihanEnding()
    {

    }

    void fenEning()
    {

    }

    void yilinEnding()
    {

    }

    void yukiEnding()
    {

    }

    void jinhuiEnding()
    {
        
    }
}
