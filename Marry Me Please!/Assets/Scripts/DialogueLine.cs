using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    [TextArea] public string dialogueText;

    public string choice1;
    public string response1;
    public UnityEvent choiceMethod1;

    public string choice2;
    public string response2;
    public UnityEvent choiceMethod2;

    public string choice3;
    public string response3;
    public UnityEvent choiceMethod3;

    public string choice4;
    public string response4;
    public UnityEvent choiceMethod4;

    public UnityEvent methodOnStart;
    public UnityEvent methodOnEnd;
}

