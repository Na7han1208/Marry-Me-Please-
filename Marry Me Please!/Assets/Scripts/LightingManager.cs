using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingManager : MonoBehaviour
{
    Color blush = new(1f, 0.506f, 0.992f);
    Color angry = new(1f, 0.106f, 0.182f);
    [SerializeField] private Light2D globalLight;

    public void StartLightBlend(Boolean mood){
        if (mood){
            StartCoroutine(BlendLight(blush));
        }
        else{
            StartCoroutine(BlendLight(angry));
        }
    } // true is blush, false is angry.

    private IEnumerator BlendLight(Color color)
    {
        float duration = 0.7f;
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            SetLightColor(Color.white, color, t);
            time += Time.deltaTime;
            yield return null;
        }

        SetLightColor(Color.white, color, 1f); //Make sure we get full target colour
        time = 0f;

        while (time < duration){
            float t = time / duration;
            SetLightColor(color, Color.white, t);
            time += Time.deltaTime;
            yield return null;
        }
        SetLightColor(color, Color.white, 1f); //Make sure we get back to white
    }

    private void SetLightColor(Color c1, Color c2, float blend){
        blend = Mathf.Clamp01(blend);
        Color blendColor = Color.Lerp(c1, c2, blend);
        globalLight.color = blendColor;
    }
}
