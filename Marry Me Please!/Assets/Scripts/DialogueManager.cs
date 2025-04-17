using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text characterNameText;
    public TMP_Text dialogueText;
    public Button[] choiceButtons;
    public TMP_Text spacebarReminder;

    [Header("Dialogue Data")]
    public DialogueLine[] dialogueLines;

    private int currentLine = 0;
    private bool awaitingSpacebar = false;

    void Start()
    {
        spacebarReminder.gameObject.SetActive(false);
        foreach(var button in choiceButtons){
            button.gameObject.SetActive(false);
        }
        ShowNextDialogue();
    }

    void Update()
    {
        // If awaiting spacebar input, wait until it's pressed
        if (awaitingSpacebar && Input.GetKeyDown(KeyCode.Space))
        {
            awaitingSpacebar = false;
            currentLine++;
            ShowNextDialogue();
            spacebarReminder.gameObject.SetActive(false);
        }
    }

    public void ShowNextDialogue()
    {
        if (currentLine >= dialogueLines.Length)
        {
            dialogueText.text = "End of conversation.";
            foreach (var button in choiceButtons) button.gameObject.SetActive(false);
            return;
        }

        DialogueLine line = dialogueLines[currentLine];

        if (line.methodOnStart != null)
            line.methodOnStart.Invoke();

        characterNameText.text = line.characterName;

        StartCoroutine(TypeText(line.dialogueText, () =>
        {
            if (line.methodOnEnd != null)
                line.methodOnEnd.Invoke();

            if (HasChoices(line))
            {
                ShowChoices(line);
            }
            else
            {
                awaitingSpacebar = true; // Wait for spacebar after dialogue is shown
            }
        }));
    }

    IEnumerator TypeText(string text, UnityAction onComplete)
    {
        dialogueText.text = "";
        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        onComplete.Invoke();
    }

    bool HasChoices(DialogueLine line)
    {
        return !string.IsNullOrEmpty(line.choice1) ||
               !string.IsNullOrEmpty(line.choice2) ||
               !string.IsNullOrEmpty(line.choice3) ||
               !string.IsNullOrEmpty(line.choice4);
    }

    void ShowChoices(DialogueLine line)
    {
        SetupButton(0, line.choice1, line.response1, line.choiceMethod1);
        SetupButton(1, line.choice2, line.response2, line.choiceMethod2);
        SetupButton(2, line.choice3, line.response3, line.choiceMethod3);
        SetupButton(3, line.choice4, line.response4, line.choiceMethod4);
    }

    void SetupButton(int index, string choiceText, string responseText, UnityEvent responseMethod)
    {
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
            // Hide all buttons after choice is selected
            foreach (var button in choiceButtons)
                button.gameObject.SetActive(false);

            // Show the response text and wait for spacebar before continuing
            StartCoroutine(TypeText(responseText, () =>
            {
                awaitingSpacebar = true; // Wait for spacebar to proceed
                spacebarReminder.gameObject.SetActive(true);
            }));

            // Invoke the response method, if any
            if (responseMethod != null)
                responseMethod.Invoke();
        });
    }
}
