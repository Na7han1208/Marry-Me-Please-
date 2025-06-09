using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BonusContentManager : MonoBehaviour
{
    [Header("Card Setup")]
    public List<Button> cardButtons;
    public int currentIndex = 0;
    public Vector2 centerPosition = Vector2.zero;
    public float spacing = 400f;
    public float moveDuration = 0.3f;
    public float sideScale = 0.7f;

    [Header("Mouse Hover Effect")]
    public float hoverRotationAmount = 10f;
    public float hoverSmoothing = 5f;

    private bool isAnimating = false;
    private Button currentCenterCard;

    void Start()
    {
        RepositionCardsImmediate();
    }

    void Update()
    {
        if (isAnimating) return;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            MoveTo(currentIndex + 1);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            MoveTo(currentIndex - 1);

        UpdateHoverEffect();
    }

    public void MoveLeft() => MoveTo(currentIndex - 1);
    public void MoveRight() => MoveTo(currentIndex + 1);

    void MoveTo(int index)
    {
        if (index < 0 || index >= cardButtons.Count) return;
        currentIndex = index;
        StartCoroutine(AnimateToPositions());
    }

    IEnumerator AnimateToPositions()
    {
        isAnimating = true;

        float t = 0f;

        var startPositions = new Vector2[cardButtons.Count];
        var targetPositions = new Vector2[cardButtons.Count];
        var startScales = new Vector3[cardButtons.Count];
        var targetScales = new Vector3[cardButtons.Count];

        for (int i = 0; i < cardButtons.Count; i++)
        {
            RectTransform rt = cardButtons[i].GetComponent<RectTransform>();
            int offset = i - currentIndex;
            bool isVisible = Mathf.Abs(offset) <= 1;

            // Set target positions and scales
            if (isVisible)
            {
                targetPositions[i] = centerPosition + Vector2.right * spacing * offset;
                targetScales[i] = (i == currentIndex) ? Vector3.one : Vector3.one * sideScale;

                // Save start positions and scales BEFORE toggling active state
                startPositions[i] = rt.anchoredPosition;
                startScales[i] = rt.localScale;
            }
            else
            {
                startPositions[i] = rt.anchoredPosition;
                startScales[i] = rt.localScale;
                targetPositions[i] = startPositions[i];
                targetScales[i] = startScales[i];
            }
        }

        // Now toggle active states in a separate loop to avoid messing with layout during data capture
        for (int i = 0; i < cardButtons.Count; i++)
        {
            int offset = i - currentIndex;
            bool isVisible = Mathf.Abs(offset) <= 1;
            cardButtons[i].gameObject.SetActive(isVisible);
            if (isVisible)
                cardButtons[i].GetComponent<RectTransform>().localRotation = Quaternion.identity;
        }


        while (t < moveDuration)
        {
            t += Time.deltaTime;
            float norm = Mathf.Clamp01(t / moveDuration);

            for (int i = 0; i < cardButtons.Count; i++)
            {
                if (!cardButtons[i].gameObject.activeSelf) continue;

                RectTransform rt = cardButtons[i].GetComponent<RectTransform>();
                rt.anchoredPosition = Vector2.Lerp(startPositions[i], targetPositions[i], norm);
                rt.localScale = Vector3.Lerp(startScales[i], targetScales[i], norm);
            }

            yield return null;
        }

        RepositionCardsImmediate();
        isAnimating = false;
    }

    void RepositionCardsImmediate()
    {
        for (int i = 0; i < cardButtons.Count; i++)
        {
            RectTransform rt = cardButtons[i].GetComponent<RectTransform>();

            int offset = i - currentIndex;
            bool isVisible = Mathf.Abs(offset) <= 1;

            cardButtons[i].gameObject.SetActive(isVisible);

            if (isVisible)
            {
                rt.anchoredPosition = centerPosition + Vector2.right * spacing * offset;
                rt.localScale = (i == currentIndex) ? Vector3.one : Vector3.one * sideScale;
                rt.localRotation = Quaternion.identity;
                cardButtons[i].interactable = (i == currentIndex);
            }
        }

        currentCenterCard = cardButtons[currentIndex];
    }

    void UpdateHoverEffect()
    {
        if (currentCenterCard == null || !currentCenterCard.gameObject.activeSelf)
            return;

        RectTransform rt = currentCenterCard.GetComponent<RectTransform>();

        Vector3 worldPos = rt.position;
        Vector2 cardScreenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);

        Vector2 mousePos = Input.mousePosition;
        Vector2 offset = (mousePos - cardScreenPos) / 100f;

        float rotX = Mathf.Clamp(-offset.y * hoverRotationAmount, -hoverRotationAmount, hoverRotationAmount);
        float rotY = Mathf.Clamp(offset.x * hoverRotationAmount, -hoverRotationAmount, hoverRotationAmount);

        Quaternion targetRot = Quaternion.Euler(rotX, rotY, 0f);
        rt.localRotation = Quaternion.Slerp(rt.localRotation, targetRot, Time.deltaTime * hoverSmoothing);
    }

    public void Load3CupMonty()
    {
        PlayerPrefs.SetInt("RouteFromMenu", 1);
        SceneManager.LoadScene("3CupMonty");
    }

    public void LoadArchery()
    {
        PlayerPrefs.SetInt("RouteFromMenu", 1);
        SceneManager.LoadScene("Archery");
    }

    public void LoadMahjong()
    {
        PlayerPrefs.SetInt("RouteFromMenu", 1);
        SceneManager.LoadScene("Archery");
    }
}

