using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ArcheryManager : MonoBehaviour
{
    [Header("Cursor Settings")]
    public RectTransform cursorImage;     
    public float jitterRadius = 100f;       
    public float jitterSpeed = 5f;         
    public float cursorSmoothSpeed = 10f;   
    public float holdTimeRequired = 2f;     

    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    private Canvas canvas;

    private bool isCharging = false;
    private float holdTimer = 0f;
    private float jitterTimer = 0f;

    private Vector3 targetJitterPos;

    void Start()
    {
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
            SetNewJitterTarget(); // Set initial target
        }

        if (isCharging && Input.GetMouseButton(0))
        {
            holdTimer += Time.deltaTime;
            HandleJitterSmooth();
        }

        if (isCharging && Input.GetMouseButtonUp(0))
        {
            if (holdTimer >= holdTimeRequired)
            {
                TryShootAtButton();
            }
            else
            {
                Debug.Log("Released too early.");
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
        );

        Vector2 smoothPos = Vector2.Lerp(currentPos, localTarget, Time.deltaTime * cursorSmoothSpeed);
        cursorImage.anchoredPosition = smoothPos;
    }

    void SetNewJitterTarget()
    {
        Vector3 basePos = Input.mousePosition;
        Vector2 offset = Random.insideUnitCircle * jitterRadius;
        targetJitterPos = basePos + (Vector3)offset;

        // Optional: Clamp to screen bounds
        targetJitterPos.x = Mathf.Clamp(targetJitterPos.x, 0, Screen.width);
        targetJitterPos.y = Mathf.Clamp(targetJitterPos.y, 0, Screen.height);
    }

    void TryShootAtButton()
    {
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = cursorImage.position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (var result in results)
        {
            Button button = result.gameObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.Invoke();
                Debug.Log("Hit button: " + button.name);
                return;
            }
        }

        Debug.Log("Missed all buttons.");
    }
}
