using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_Text affinityTxt;

    void Start()
    {
        SaveData data = SaveLoadManager.Instance.LoadGame();
        affinityTxt.text =  "Fen:\t" + data.fenAffinity +
                            "\nMing:\t" + data.mingAffinity +
                            "\nJinhui:\t" + data.jinhuiAffinity +
                            "\nTheo:\t" + data.theodoreAffinity;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
