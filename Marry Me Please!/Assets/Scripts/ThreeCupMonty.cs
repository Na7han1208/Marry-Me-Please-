using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ThreeCardMonty : MonoBehaviour
{
    //INIT
    [SerializeField] Button[] cards;
    private int winningCardIndex;
    private Vector3[] originalPositions;
    [SerializeField] float duration = 0.5f;
    [SerializeField] int numMovements = 7; 
    [SerializeField] TMP_Text AboveCupsText;

    void Start()
    {
        // Store original positions
        originalPositions = new Vector3[cards.Length];
        for (int i = 0; i < cards.Length; i++){
            originalPositions[i] = cards[i].GetComponent<RectTransform>().anchoredPosition;
        }
        winningCardIndex = Random.Range(0, cards.Length);
        AboveCupsText.text = "Watch the cups!";
        StartCoroutine(ShowWinningCardThenShuffle());
    }

    IEnumerator ShowWinningCardThenShuffle(){
        // Temporarily turn winning card red
        Image cardImage = cards[winningCardIndex].GetComponent<Image>();
        Color originalColor = cardImage.color;
        cardImage.color = Color.red;
        yield return new WaitForSeconds(1.5f); // Show red briefly
        cardImage.color = originalColor;

        // Start the shuffle animation
        yield return StartCoroutine(ShuffleAndStart());
    }

    IEnumerator ShuffleAndStart()
    {
        DisableAllCards();
        yield return new WaitForSeconds(3f);
        AboveCupsText.text = "";
        Debug.Log("Shuffling...");
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

            // Animate cards moving to new positions
            for (float t = 0; t < duration; t += Time.deltaTime){
                float lerpFactor = t / duration;
                for (int i = 0; i < cards.Length; i++){
                    RectTransform rect = cards[i].GetComponent<RectTransform>();
                    rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, positions[i], lerpFactor);
                }
                yield return null;
            }

            // Snap to final positions
            for (int i = 0; i < cards.Length; i++){
                cards[i].GetComponent<RectTransform>().anchoredPosition = positions[i];
            }

        }
        // Randomize winning card
        winningCardIndex = Random.Range(0, cards.Length);

        // Re-enable interactions
        for (int i = 0; i < cards.Length; i++){
            int index = i;
            cards[i].onClick.RemoveAllListeners();
            cards[i].onClick.AddListener(() => OnCardSelected(index));
        }
        EnableAllCards();
    }

void OnCardSelected(int selectedIndex){
    // Reveal winning card by turning it red
    cards[winningCardIndex].GetComponent<Image>().color = Color.red;
    DisableAllCards();
}


    void DisableAllCards(){
        foreach (var card in cards){
            card.interactable = false;
        }
    }

    void EnableAllCards(){
        foreach (var card in cards){
            card.interactable = true;
        }
    }
}
