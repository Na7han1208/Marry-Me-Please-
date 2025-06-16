using UnityEngine;
using UnityEngine.UI;

public class CircleRaycastTarget : Image
{
    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        RectTransform rect = transform as RectTransform;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPoint, eventCamera, out localPoint);

        // Normalize to center
        Vector2 normalized = new Vector2(
            localPoint.x / rect.rect.width,
            localPoint.y / rect.rect.height
        ) * 2f;

        float radius = 0.5f;
        return normalized.sqrMagnitude <= radius * radius;
    }
}
