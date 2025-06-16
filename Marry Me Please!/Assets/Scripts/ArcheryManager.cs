using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ArcheryManager : MonoBehaviour
{
    [Header("Cursor Settings")]
    public RectTransform cursorImage;
    public float jitterRadius = 100f;
    public float jitterSpeed = 5f;
    public float cursorSmoothSpeed = 1000f;
    public float holdTimeRequired = 1.3f;

    [Header("Hitmarker")]
    public GameObject hitmarkerPrefab;
    public RectTransform hitmarkerParent;

    int shotsRemaining = 5;
    int totalScore = 0;

    public Slider chargeSlider;
    public TMP_Text scoreText;
    public TMP_Text shotsRemainingText;

    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    private Canvas canvas;

    private bool isCharging = false;
    private float holdTimer = 0f;
    private float jitterTimer = 0f;

    private Vector3 targetJitterPos;

    void Start()
    {
        AudioManager.Instance.StopAll();
        AudioManager.Instance.Play("3CMMusic");

        chargeSlider.maxValue = holdTimeRequired;
        scoreText.text = "0";
        shotsRemainingText.text = "IIIII";

        canvas = FindFirstObjectByType<Canvas>();
        raycaster = canvas.GetComponent<GraphicRaycaster>();
        eventSystem = EventSystem.current;

        cursorImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            holdTimer = 0f;
            jitterTimer = 0f;
            cursorImage.gameObject.SetActive(true);
            SetNewJitterTarget();
        }

        if (isCharging && Input.GetMouseButton(0))
        {
            holdTimer += Time.deltaTime;
            chargeSlider.value = holdTimer;
            HandleJitterSmooth();
        }

        if (isCharging && Input.GetMouseButtonUp(0))
        {
            if (holdTimer >= holdTimeRequired)
            {
                AudioManager.Instance.Play("BowLoad");
                TryShootAtButton();
            }
            else
            {
                Debug.Log("Released too early.");
                //Some kind of feedback other than console, maybe a sound or something ****************
            }

            isCharging = false;
            cursorImage.gameObject.SetActive(false);
        }
    }

    void HandleJitterSmooth()
    {
        jitterTimer += Time.deltaTime;

        // Update jitter target periodically
        if (jitterTimer >= 1f / jitterSpeed)
        {
            SetNewJitterTarget();
            jitterTimer = 0f;
        }

        // Smooth movement toward the jittered position
        Vector2 currentPos = cursorImage.anchoredPosition;

        Vector2 localTarget;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            targetJitterPos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out localTarget
        ); //under no circumstances should anyone touch this code, i works, how? idk, but it does

        Vector2 smoothPos = Vector2.Lerp(currentPos, localTarget, Time.deltaTime * cursorSmoothSpeed);
        cursorImage.anchoredPosition = smoothPos;
    }

    void SetNewJitterTarget()
    {
        Vector3 basePos = Input.mousePosition;
        Vector2 offset = UnityEngine.Random.insideUnitCircle * jitterRadius;
        targetJitterPos = basePos + (Vector3)offset;

        targetJitterPos.x = Mathf.Clamp(targetJitterPos.x, 0, Screen.width);
        targetJitterPos.y = Mathf.Clamp(targetJitterPos.y, 0, Screen.height);
    }

    void TryShootAtButton()
    {
        shotsRemaining--;
        String text = "";
        for (int i = 0; i < shotsRemaining; i++)
        {
            text += "I";
        } shotsRemainingText.text = text;
        if (shotsRemaining <= 0)
        {
            endGame();
            return;
        }
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = cursorImage.position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        // Sort by depth
        results.Sort((a, b) => b.depth.CompareTo(a.depth)); //yeah this doesn't work, so pretend the game has rng

        foreach (var result in results)
        {
            Button button = result.gameObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.Invoke();
                SpawnHitmarker(cursorImage.position);
                AudioManager.Instance.Play("ArrowHit");
                return; // Exit after the first valid hit
            }
        }
        Debug.Log("Missed all buttons.");
    }

    public void ChangeScore(int score)
    {
        totalScore += score;
        scoreText.text = totalScore + "";
    }

    void SpawnHitmarker(Vector3 screenPos)
    {
        if (hitmarkerPrefab == null || hitmarkerParent == null) return;

        // Instantiate hit marker
        GameObject marker = Instantiate(hitmarkerPrefab, hitmarkerParent);

        // Convert screen position to local canvas position
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            hitmarkerParent,
            screenPos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out localPoint
        );

        RectTransform rt = marker.GetComponent<RectTransform>();
        rt.anchoredPosition = localPoint;
    }


    void endGame()
    {
        //add affinity
        if (totalScore >= 400)
        {
            SaveData existingData = SaveLoadManager.Instance.LoadGame();
            SaveData saveData = new SaveData();

            saveData.currentLine = existingData.currentLine;
            saveData.playerName = existingData.playerName;

            saveData.mingAffinity = existingData.mingAffinity + 8;
            saveData.jinhuiAffinity = existingData.jinhuiAffinity + 8;
            saveData.yilinAffinity = existingData.yilinAffinity + 8;
            saveData.fenAffinity = existingData.fenAffinity + 8;
            saveData.yukiAffinity = existingData.yukiAffinity + 8;
            saveData.theodoreAffinity = existingData.theodoreAffinity + 8;
            saveData.zihanAffinity = existingData.zihanAffinity + 8;
            SaveLoadManager.Instance.SaveGame(saveData);
        }
        else
        {
            SaveData existingData = SaveLoadManager.Instance.LoadGame();
            SaveData saveData = new SaveData();

            saveData.currentLine = existingData.currentLine;
            saveData.playerName = existingData.playerName;

            saveData.mingAffinity = existingData.mingAffinity - 8;
            saveData.jinhuiAffinity = existingData.jinhuiAffinity - 8;
            saveData.yilinAffinity = existingData.yilinAffinity - 8;
            saveData.fenAffinity = existingData.fenAffinity - 8;
            saveData.yukiAffinity = existingData.yukiAffinity - 8;
            saveData.theodoreAffinity = existingData.theodoreAffinity - 8;
            saveData.zihanAffinity = existingData.zihanAffinity - 8;
            SaveLoadManager.Instance.SaveGame(saveData);
        }

        //return or continue
        if (PlayerPrefs.GetInt("RouteFromMenu") == 1)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
    }
}
