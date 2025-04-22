using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEngine.UIElements;
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
        // Store original positions
        originalPositions = new Vector3[Cups.Length];
        for (int i = 0; i < Cups.Length; i++){
            originalPositions[i] = Cups[i].GetComponent<RectTransform>().anchoredPosition;
        }
        winningCupIndex = Random.Range(0, Cups.Length);
        AboveCupsText.text = "Watch the cups!";
        StartCoroutine(ShowWinningCupThenShuffle());
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            returnToMenu();
        }
    }

    IEnumerator ShowWinningCupThenShuffle(){
        // Temporarily turn winning Cup red (Later will show where the dumplings are)
        Image CupImage = Cups[winningCupIndex].GetComponent<Image>();
        Color originalColor = CupImage.color;
        CupImage.color = Color.red;
        yield return new WaitForSeconds(1.5f); // Show red briefly
        CupImage.color = originalColor;

        // Start the shuffle animation
        yield return StartCoroutine(ShuffleAndStart());
    }

    IEnumerator ShuffleAndStart()
    {
        DisableAllCups();
        yield return new WaitForSeconds(3f);
        AboveCupsText.text = "";
        for(int k = 0; k < numMovements; k++){
            // Shuffle target positions
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
        // Randomize winning Cup
        winningCupIndex = Random.Range(0, Cups.Length);

        // Re-enable interactions
        for (int i = 0; i < Cups.Length; i++){
            int index = i;
            Cups[i].onClick.RemoveAllListeners();
            Cups[i].onClick.AddListener(() => OnCupSelected(index));
        }
        EnableAllCups();
    }

void OnCupSelected(int selectedIndex){
    // Reveal winning Cup by turning it red
    Cups[winningCupIndex].GetComponent<Image>().color = Color.red;
    DisableAllCups();
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
        SceneManager.LoadScene("MainMenu");
    }
}
