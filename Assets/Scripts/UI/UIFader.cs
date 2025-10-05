using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UIFader : MonoBehaviour
{
    List<CanvasRenderer> _renderers = new();

    private void Awake()
    {
        _renderers = GetComponentsInChildren<CanvasRenderer>().ToList();
    }

    private void SetAlpha(float value)
    {
        _renderers.ForEach((e) => e.SetAlpha(value));
    }

    private IEnumerator FadeCoroutine(float duration, float stop, Action then)
    {
        float elapsedTime = 0;
        float start = 1 - stop;

        while (elapsedTime < duration)
        {
            float k = elapsedTime / duration;
            float alpha = Mathf.Lerp(start, stop, Easing.InSine(k));
            SetAlpha(alpha);
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        SetAlpha(stop);

        then?.Invoke();
    }

    public void FadeOut(float duration, Action then = null)
    {
        try
        {
            StartCoroutine(FadeCoroutine(duration, 0, then));
        }
        catch
        {
            //
        }
    }

    public void FadeIn(float duration, Action then = null)
    {
        StartCoroutine(FadeCoroutine(duration, 1, then));
    }
}
