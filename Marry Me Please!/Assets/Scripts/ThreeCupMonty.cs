using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class ThreeCupMonty : MonoBehaviour
{
    //INIT
    [SerializeField] Button[] Cups;
    private int winningCupIndex;
    private Vector3[] originalPositions;
    [SerializeField] float duration = 0.5f;
    [SerializeField] int numMovements = 7; 
    [SerializeField] TMP_Text AboveCupsText;

    void Start(){
        AudioManager.Instance.StopAll();
        AudioManager.Instance.Play("3CMMusic");
        Cups[1].GetComponent<Animator>().StopPlayback();

        // Store original positions
        originalPositions = new Vector3[Cups.Length];
        for (int i = 0; i < Cups.Length; i++)
        {
            originalPositions[i] = Cups[i].GetComponent<RectTransform>().anchoredPosition;
        }
        winningCupIndex = 1;
        AboveCupsText.text = "Watch the cups!";
        StartCoroutine(ShowWinningCupThenShuffle());
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            returnToMenu();
        }
    }

    IEnumerator ShowWinningCupThenShuffle(){
        Cups[winningCupIndex].GetComponent<Animator>().Play(0);
        // Start the shuffle animation
        yield return StartCoroutine(ShuffleAndStart());
    }

    IEnumerator ShuffleAndStart(){
        DisableAllCups();
        yield return new WaitForSeconds(5f);
        AboveCupsText.text = "";
        for(int k = 0; k < numMovements; k++){
            // Shuffle target positions
            AudioManager.Instance.Play("CupMove");
            List<Vector3> positions = new List<Vector3>(originalPositions);
            System.Random rng = new System.Random();
            for (int i = positions.Count - 1; i > 0; i--){
                int j = rng.Next(i + 1);
                Vector3 temp = positions[i];
                positions[i] = positions[j];
                positions[j] = temp;
            }

            // Animate Cups moving to new positions
            for (float t = 0; t < duration; t += Time.deltaTime){
                float lerpFactor = t / duration;
                for (int i = 0; i < Cups.Length; i++){
                    RectTransform rect = Cups[i].GetComponent<RectTransform>();
                    rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, positions[i], lerpFactor);
                }
                yield return null;
            }

            // Snap to final positions
            for (int i = 0; i < Cups.Length; i++){
                Cups[i].GetComponent<RectTransform>().anchoredPosition = positions[i];
            }

        }

        // Re-enable interactions
        for (int i = 0; i < Cups.Length; i++){
            int index = i;
            Cups[i].onClick.RemoveAllListeners();
            Cups[i].onClick.AddListener(() => StartCoroutine(OnCupSelected(index)));
        }
        EnableAllCups();
    }

    private IEnumerator OnCupSelected(int selectedIndex)
    {
        Cups[winningCupIndex].GetComponent<Animator>().Play(0);
        DisableAllCups();
        PlayerPrefs.SetInt("3CMUnlocked", 1);
        SaveData data = SaveLoadManager.Instance.LoadGame();
        if (selectedIndex == winningCupIndex)
        {
            AboveCupsText.text = "You win!";
            data.mingAffinity += 5;
            data.jinhuiAffinity += 5;
            data.fenAffinity += 5;
            data.theodoreAffinity += 5;
        }
        else
        {
            AboveCupsText.text = "You lose.";
            data.mingAffinity += -5;
            data.jinhuiAffinity += -5;
            data.fenAffinity += -5;
            data.theodoreAffinity += -5;
        }
        SaveLoadManager.Instance.SaveGame(data);
        yield return new WaitForSeconds(3f);
        AudioManager.Instance.StopAll();
        SceneManager.LoadScene("Main");
    }

    void DisableAllCups(){
        foreach (var Cup in Cups){
            Cup.interactable = false;
        }
    }

    void EnableAllCups(){
        foreach (var Cup in Cups){
            Cup.interactable = true;
        }
    }

    public void returnToMenu(){
        AudioManager.Instance.StopAll();
        SceneManager.LoadScene("MainMenu");
    }
}