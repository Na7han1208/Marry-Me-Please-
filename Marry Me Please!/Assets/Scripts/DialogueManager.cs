using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using System;

public class DialogueManager : MonoBehaviour
{    
    [Header("Affinity Data")]
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
    private SpriteManager spriteManager;

   

    void Start(){
        //Initialise important things to be inactive then call the first bit of dialogue
        spacebarReminder.gameObject.SetActive(false);
        foreach(var button in choiceButtons){
            button.gameObject.SetActive(false);
        }
        ShowNextDialogue();

        //Dialogue index used for referencing dialogue.
        for(int i = 0; i < dialogueLines.Length; i++){
            dialogueLines[i].dialogueIndex = i;
        }
    }

    void Update(){
        if (awaitingSpacebar && Input.GetKeyDown(KeyCode.Space)){
            awaitingSpacebar = false;
            currentLine++;
            ShowNextDialogue();
            spacebarReminder.gameObject.SetActive(false);
        }
    }

    public void ShowNextDialogue(){
        //Makes sure we're not out of bounds, and if we are just displays end of convo
        if (currentLine >= dialogueLines.Length){
            dialogueText.text = "End of conversation.";
            foreach (var button in choiceButtons) button.gameObject.SetActive(false);
            return;
        }

        DialogueLine line = dialogueLines[currentLine];

        if (line.methodOnStart != null){
            line.methodOnStart.Invoke();
        }
        characterNameText.text = line.characterName;

        StartCoroutine(TypeText(line.dialogueText, () =>
        {
            if (line.methodOnEnd != null){
                line.methodOnEnd.Invoke();
            }
            if (HasChoices(line)){
                ShowChoices(line);
            }
            else{
                awaitingSpacebar = true; 
            }
        }));
    }

    IEnumerator TypeText(string text, UnityAction onComplete){
        dialogueText.text = "";
        foreach (char c in text){
            dialogueText.text += c;
            yield return new WaitForSeconds(dialogueSpeed);
        }
        onComplete.Invoke();
    }

    //Checks if the choices are empty, if all of them are empty it returns false
    bool HasChoices(DialogueLine line){
        return !string.IsNullOrEmpty(line.choice1) ||
               !string.IsNullOrEmpty(line.choice2) ||
               !string.IsNullOrEmpty(line.choice3) ||
               !string.IsNullOrEmpty(line.choice4);
    }

    //Calls the set up button method for each button
    void ShowChoices(DialogueLine line){
        SetupButton(0, line.choice1, line.response1, line.choiceMethod1, line.affection1);
        SetupButton(1, line.choice2, line.response2, line.choiceMethod2, line.affection2);
        SetupButton(2, line.choice3, line.response3, line.choiceMethod3, line.affection3);
        SetupButton(3, line.choice4, line.response4, line.choiceMethod4, line.affection4);
    }

    //Takes the index of the button, the text on the button, the response it will display, and the method that will run if clicked and initialises all of it
    void SetupButton(int index, string choiceText, string responseText, UnityEvent responseMethod, int affinityChange){
        //Make sure we don't set a null button... no nullPointerExceptions for us!
        if (string.IsNullOrEmpty(choiceText)){
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
            foreach (var button in choiceButtons){
                button.gameObject.SetActive(false);
            }

            changeAffinity(affinityChange);
                
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

    

    public void changeAffinity(int affinity){
        switch(characterNameText.text){
            case "Ming":        mingAffinity+=affinity;         break;
            case "Jinhui":      jinhuiAffinity+=affinity;       break;
            case "Yilin":       yilinAffinity+=affinity;        break;
            case "Fen":         fenAffinity+=affinity;          break;
            case "Yuki":        yukiAffinity+=affinity;         break;
            case "Theodore":    theodoreAffinity+=affinity;     break;
            case "Zihan":       zihanAffinity+=affinity;        break;
            default: Debug.Log("Character not found");          break;
            
        }
        //DEBUG
        Debug.Log("Ming:\t" + mingAffinity + "\nJinhui:\t" + jinhuiAffinity + "\nYilin:\t" + yilinAffinity + "\nFen:\t" + fenAffinity);
    }


    //Methods called by dialogue \/
    public void load3CupMonty(){
        SceneManager.LoadScene("3CupMonty");
    }

    
    //This allows me to use the WaitForSeconds method even when not in an IEnumerator
    public void wait(float time){
        StartCoroutine(waitOutsideCoroutine(time));
    }

    public IEnumerator waitOutsideCoroutine(float time){
        yield return new WaitForSeconds(time);
    }

    public void changeSprite(int moodIndex){
        spriteManager.changeSprite(moodIndex, characterNameText.text);
    }
}

/*
    _
.__(.)< (meow)
 \__)

*/