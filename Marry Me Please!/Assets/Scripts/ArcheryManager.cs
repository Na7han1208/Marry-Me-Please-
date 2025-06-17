using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Collections;

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
    private bool isWaitingForMouseRelease = false;
    private bool isShooting = false;
    private bool isAllowingHits = false; 
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
        if (isShooting)
            return;

        if (isWaitingForMouseRelease)
        {
            if (!Input.GetMouseButton(0))
            {
                isWaitingForMouseRelease = false;
            }
            else
            {
                return;
            }
        }

        if (Input.GetMouseButtonDown(0) && !isCharging)
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
            isCharging = false;
            cursorImage.gameObject.SetActive(false);
            isWaitingForMouseRelease = true;

            if (holdTimer >= holdTimeRequired)
            {
                StartCoroutine(HandleShot());
            }
            else
            {
                Debug.Log("Released too early.");
                // ***** ADD SOUND
            }
        }
    }

    IEnumerator HandleShot()
    {
        isShooting = true;
        isAllowingHits = true; // allow scoring only in this window

        AudioManager.Instance.Play("BowLoad");

        shotsRemaining--;

        shotsRemainingText.text = new string('I', shotsRemaining);

        yield return new WaitForSeconds(0.1f); // slight sync delay

        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = cursorImage.position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        results.Sort((a, b) => b.depth.CompareTo(a.depth));

        bool hit = false;

        foreach (var result in results)
        {
            Button button = result.gameObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.Invoke();
                SpawnHitmarker(cursorImage.position);
                AudioManager.Instance.Play("ArrowHit");
                hit = true;
                break;
            }
        }

        if (!hit)
        {
            Debug.Log("Missed all buttons.");
        }

        yield return new WaitForSeconds(0.5f);

        isAllowingHits = false; // block further scoring
        isShooting = false;

        if (shotsRemaining <= 0)
        {
            EndGame();
        }
    }

    void HandleJitterSmooth()
    {
        jitterTimer += Time.deltaTime;

        if (jitterTimer >= 1f / jitterSpeed)
        {
            SetNewJitterTarget();
            jitterTimer = 0f;
        }

        Vector2 currentPos = cursorImage.anchoredPosition;

        Vector2 localTarget;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            targetJitterPos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out localTarget
        );

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

    // only allow scoring during proper shot phase
    public void ChangeScore(int score)
    {
        if (!isAllowingHits)
        {
            return;
        }

        totalScore += score;
        scoreText.text = totalScore.ToString();
    }

    void SpawnHitmarker(Vector3 screenPos)
    {
        if (hitmarkerPrefab == null || hitmarkerParent == null)
            return;

        GameObject marker = Instantiate(hitmarkerPrefab, hitmarkerParent);

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

    void EndGame()
    {
        StartCoroutine(Continue());
    }

    IEnumerator Continue()
    {
        yield return new WaitForSeconds(3);

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