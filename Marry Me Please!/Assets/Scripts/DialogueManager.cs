using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using System;
using NUnit.Framework;
//using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    //INIT
    //[Header("Affinity Data")]
    private int mingAffinity = 0;
    private int jinhuiAffinity = 0;
    private int yilinAffinity = 0;
    private int fenAffinity = 0;
    private int yukiAffinity = 0;
    private int theodoreAffinity = 0;
    private int zihanAffinity = 0;

    [Header("UI References")]
    public TMP_Text characterNameText;
    public Image characterSprite;
    public TMP_Text dialogueText;
    public Button[] choiceButtons;
    public TMP_Text spacebarReminder;

    [Header("Dialogue Data")]
    public DialogueLine[] dialogueLines;

    private int currentLine = 0;
    private bool awaitingSpacebar = false;
    public float dialogueSpeed = 0.015f;
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    [SerializeField] private SpriteManager spriteManager;
    [SerializeField] private LightingManager lightingManager;
    [SerializeField] private Image[] Banners;

    [SerializeField] private GameObject NameBox;
    private Vector2 ogNameLocation;
    private Vector2 newNameLocation;

    void Start()
    {
        //Hide all banners
        foreach (Image b in Banners)
        {
            b.enabled = false;
        }

        ogNameLocation = NameBox.transform.position;
        newNameLocation = new Vector2(466, NameBox.transform.position.y);

        AudioManager.Instance.Play("InGameMusic");
        //Dialogue index used for referencing dialogue.
        for (int i = 0; i < dialogueLines.Length; i++)
        {
            dialogueLines[i].dialogueIndex = i;
        }
        spacebarReminder.gameObject.SetActive(false);
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
        if (SaveLoadManager.Instance.SaveExists())
        {
            SaveData data = SaveLoadManager.Instance.LoadGame();
            currentLine = data.currentLine;
            mingAffinity = data.mingAffinity;
            jinhuiAffinity = data.jinhuiAffinity;
            yilinAffinity = data.yilinAffinity;
            fenAffinity = data.fenAffinity;
            yukiAffinity = data.yukiAffinity;
            theodoreAffinity = data.theodoreAffinity;
            zihanAffinity = data.zihanAffinity;
            ChangeSprite(0); //Load neutral state of current character
        }
        else
        {
            currentLine = 0; // start fresh if no save
        }

        ShowNextDialogue();
        StartCoroutine(AutoSave(20f));
    }

    void Update()
    {
        //Spacebar Continue
        if (awaitingSpacebar && !isTyping && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            awaitingSpacebar = false;
            currentLine++;
            ShowNextDialogue();
            spacebarReminder.gameObject.SetActive(false);
        }

        //Dialogue Options through Nums
        if (choiceButtons[0].IsActive() && Input.GetKeyDown(KeyCode.Alpha1))
        {
            choiceButtons[0].onClick.Invoke();
        }
        if (choiceButtons[1].IsActive() && Input.GetKeyDown(KeyCode.Alpha2))
        {
            choiceButtons[1].onClick.Invoke();
        }
        if (choiceButtons[2].IsActive() && Input.GetKeyDown(KeyCode.Alpha3))
        {
            choiceButtons[2].onClick.Invoke();
        }
        if (choiceButtons[3].IsActive() && Input.GetKeyDown(KeyCode.Alpha4))
        {
            choiceButtons[3].onClick.Invoke();
        }

        //Return to Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            returnToMenu();
        }
    }


    public void ShowNextDialogue()
    {
        // Kill the method if already typing
        if (isTyping) return;

        // End convo if out of lines
        if (currentLine >= dialogueLines.Length)
        {
            dialogueText.text = "End of conversation.";
            foreach (var button in choiceButtons) button.gameObject.SetActive(false);
            AudioManager.Instance.StopAll();
            SceneManager.LoadScene("MainMenu");
            return;
        }

        DialogueLine line = dialogueLines[currentLine];

        if (line.methodOnStart != null)
        {
            line.methodOnStart.Invoke();
        }
        Color tempColor = characterSprite.GetComponent<Image>().color;
        if (line.characterName == "MC")
        {
            //NameBox.transform.position = newNameLocation;
            Debug.Log("Loaded name: " + SaveLoadManager.Instance.LoadGame().playerName);
            characterNameText.text = SaveLoadManager.Instance.LoadGame().playerName;
        }
        else
        {
            //NameBox.transform.position = ogNameLocation;
            characterNameText.text = line.characterName;
        }

        if (line.characterName == "thought")
        {
            NameBox.SetActive(false);
            line.dialogueText = "<I>" + line.dialogueText + "</I>";
        }
        else
        {
            NameBox.SetActive(true);
        }

        if (characterSprite.sprite == null)
        {
            tempColor.a = 0f;
            characterSprite.GetComponent<Image>().color = tempColor;
        }
        else
        {
            tempColor.a = 1f;
            characterSprite.GetComponent<Image>().color = tempColor;
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        // Start typing coroutine
        isTyping = true;
        typingCoroutine = StartCoroutine(TypeText(line.dialogueText, () =>
        {
            if (HasChoices(line))
            {
                ShowChoices(line);
            }
            else
            {
                awaitingSpacebar = true;
                spacebarReminder.gameObject.SetActive(true);
            }
            StartCoroutine(WaitForSpacebarThenInvoke(line));
        }));
    }

    IEnumerator WaitForSpacebarThenInvoke(DialogueLine line)
    {
        // Wait until space is pressed
        yield return new WaitUntil(() => !awaitingSpacebar);

        if (line.methodOnEnd != null)
        {
            line.methodOnEnd.Invoke();
        }
    }

    IEnumerator TypeText(string text, UnityAction onComplete)
    {
        dialogueText.text = "";
        float adjustedSpeed = dialogueSpeed / PlayerPrefs.GetFloat("DialogueSpeed", 1f);

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(adjustedSpeed);
        }

        isTyping = false;
        onComplete.Invoke();
    }

    //Checks if the choices are empty, if all of them are empty it returns false
    bool HasChoices(DialogueLine line)
    {
        return !string.IsNullOrEmpty(line.choice1) ||
               !string.IsNullOrEmpty(line.choice2) ||
               !string.IsNullOrEmpty(line.choice3) ||
               !string.IsNullOrEmpty(line.choice4);
    }

    //Calls the set up button method for each button
    void ShowChoices(DialogueLine line)
    {
        SetupButton(0, line.choice1, line.response1, line.choiceMethod1, line.affection1);
        SetupButton(1, line.choice2, line.response2, line.choiceMethod2, line.affection2);
        SetupButton(2, line.choice3, line.response3, line.choiceMethod3, line.affection3);
        SetupButton(3, line.choice4, line.response4, line.choiceMethod4, line.affection4);
    }

    //Takes the index of the button, the text on the button, the response it will display, and the method that will run if clicked and initialises all of it
    void SetupButton(int index, string choiceText, string responseText, UnityEvent responseMethod, int affinityChange)
    {
        //Make sure we don't set a null button... no nullPointerExceptions for us!
        if (string.IsNullOrEmpty(choiceText))
        {
            choiceButtons[index].gameObject.SetActive(false);
            return;
        }

        choiceButtons[index].gameObject.SetActive(true);
        TMP_Text buttonText = choiceButtons[index].GetComponentInChildren<TMP_Text>();
        buttonText.text = choiceText;

        choiceButtons[index].onClick.RemoveAllListeners();
        choiceButtons[index].onClick.AddListener(() =>
        {
            //Hides all the buttons after a button is pressed
            foreach (var button in choiceButtons)
            {
                button.gameObject.SetActive(false);
            }

            changeAffinity(characterNameText.text, affinityChange);

            //Shows the applicable response text and then waits for spacebar to continue, also shows the player a reminder to press spacebar to continue
            StartCoroutine(TypeText(responseText, () =>
            {
                awaitingSpacebar = true; // 
                spacebarReminder.gameObject.SetActive(true);
            }));

            //If there is a response method, invoke that bitch
            if (responseMethod != null)
                responseMethod.Invoke();
        });
    }

    public void changeAffinity(string characterName, int affinity)
    {
        switch (characterName)
        {
            case "Ming": mingAffinity += affinity; break;
            case "Jinhui": jinhuiAffinity += affinity; break;
            case "Yilin": yilinAffinity += affinity; break;
            case "Fen": fenAffinity += affinity; break;
            case "Yuki": yukiAffinity += affinity; break;
            case "Theodore": theodoreAffinity += affinity; break;
            case "Zihan": zihanAffinity += affinity; break;
            default: Debug.Log("Character not found"); break;
        }
        if (affinity >= 8)
        {
            lightingManager.StartLightBlend(true);
        }
        else if (affinity <= -8)
        {
            lightingManager.StartLightBlend(false);
        }
    }

    public void load3CupMonty()
    {
        SaveDialogueState();
        SceneManager.LoadScene("3CupMonty");
    }

    public void loadMahjong()
    {
        SaveDialogueState();
        SceneManager.LoadScene("Mahjong");
    }

    public void loadArchery()
    {
        SaveDialogueState();
        SceneManager.LoadScene("Archery");
    }

    //This allows me to use the WaitForSeconds method even when not in an IEnumerator
    public void wait(float time)
    {
        StartCoroutine(waitOutsideCoroutine(time));
    }

    public IEnumerator waitOutsideCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void ChangeSprite(int moodIndex)
    {
        characterSprite.sprite = spriteManager.changeSprite(moodIndex, dialogueLines[currentLine].characterName);
        Debug.Log("Change Sprite Called:\nMoodIndex:\t" + moodIndex + "Character Name:\t" + dialogueLines[currentLine].characterName);
    }

    public void skipToLine(int targetIndex)
    {
        if (targetIndex >= 0 && targetIndex < dialogueLines.Length)
        {
            // Stop any ongoing typing coroutine
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            // Reset the dialogue text and choices
            dialogueText.text = "";
            foreach (var button in choiceButtons)
            {
                button.gameObject.SetActive(false);
            }

            // Set the current line and display the new dialogue
            currentLine = targetIndex;
            ShowNextDialogue();
        }
    }

    public void returnToMenu()
    {
        SaveDialogueState();
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.StopAll();
    }


    public void SaveDialogueState()
    {
        SaveData existingData = SaveLoadManager.Instance.LoadGame();
        SaveData saveData = new SaveData();
        saveData.playerName = existingData.playerName;

        if (currentLine > 3)
        {
            saveData.currentLine = currentLine - 2;
        }
        else
        {
            saveData.currentLine = currentLine;
        }

        saveData.mingAffinity = mingAffinity;
        saveData.jinhuiAffinity = jinhuiAffinity;
        saveData.yilinAffinity = yilinAffinity;
        saveData.fenAffinity = fenAffinity;
        saveData.yukiAffinity = yukiAffinity;
        saveData.theodoreAffinity = theodoreAffinity;
        saveData.zihanAffinity = zihanAffinity;

        SaveLoadManager.Instance.SaveGame(saveData);
    }

    private IEnumerator AutoSave(float delay)
    {
        while (true)
        {
            SaveDialogueState();
            yield return new WaitForSeconds(delay);
        }
    }

    public void callShowBanner(int index){
        StartCoroutine(showBanner(index));
    }

    public void loadScene(int index)
    {
        MainManager mainManager = new MainManager();
        mainManager.loadBackground(index);
    }

    public IEnumerator showBanner(int index)
    {
        Banners[index].enabled = true;
        yield return new WaitForSeconds(3f);
        Banners[index].enabled = false;
    }
}

/*
  Gerald
    _
.__(.)< (meow)
 \___)

*/


