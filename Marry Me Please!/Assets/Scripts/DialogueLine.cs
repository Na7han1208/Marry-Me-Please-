using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueLine
{
    public int dialogueIndex;
    [TextArea] public string characterName;
    [TextArea] public string dialogueText;

    [Header("Choice1")]
    [TextArea] public string choice1;
    [TextArea] public string response1;
    public int affection1;

    [Header("Choice2")]
    [TextArea] public string choice2;
    [TextArea] public string response2;
    public int affection2;

    [Header("Choice3")]
    [TextArea] public string choice3;
    [TextArea] public string response3;
    public int affection3;

    [Header("Choice4")]
    [TextArea] public string choice4;
    [TextArea] public string response4;
    public int affection4;

    [Header("Methods")]
    public UnityEvent choiceMethod1;
    public UnityEvent choiceMethod2;
    public UnityEvent choiceMethod3;
    public UnityEvent choiceMethod4;
    
    public UnityEvent methodOnStart;
    public UnityEvent methodOnEnd;
}

