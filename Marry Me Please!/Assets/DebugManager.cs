using TMPro;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private TMP_Text line;
    [SerializeField] private TMP_Text character;
    [SerializeField] private TMP_Text affinity;

    [SerializeField] private TMP_Text characterNameBox;

    void Start()
    {
        
    }

    void Update()
    {
        SaveData data = SaveLoadManager.Instance.LoadGame();
        line.text = data.currentLine + "";
        character.text = characterNameBox.text;
        switch (characterNameBox.text)
        {
            case "Ming":
                affinity.text = data.mingAffinity + "";
                break;
            case "Fen":
                affinity.text = data.fenAffinity + "";
                break;
            case "Theo":
                affinity.text = data.theodoreAffinity + "";
                break;
            case "Zihan":
                affinity.text = data.zihanAffinity + "";
                break;
            case "Yilin":
                affinity.text = data.yilinAffinity + "";
                break;
            case "Jinhui":
                affinity.text = data.jinhuiAffinity + "";
                break;
            case "Yuki":
                affinity.text = data.yukiAffinity + "";
                break;
            default:
                affinity.text = "n/a";
                break;
        }
    }
}
