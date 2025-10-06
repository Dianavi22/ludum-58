using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UIFader : MonoBehaviour
{
    List<CanvasRenderer> _renderers = new();

    private float _alphaValue;
    public bool isFadeIn => _alphaValue == 1 && _animators.All(animator => 1 < animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

    private List<Animator> _animators;
    private List<GameObject> _children;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<CanvasRenderer>(true).ToList();
        _animators = GetComponentsInChildren<Animator>(true).ToList();
        _children = GetComponentsInChildren<Transform>(true).Select(transform => transform.gameObject).ToList();
        SetAlpha(0);
        ActivateChildren(false);
    }

    private void SetAlpha(float value)
    {
        _renderers.ForEach((e) => e.SetAlpha(value));
        _alphaValue = value;
    }

    private void ActivateChildren(bool active)
    {
        _children.ForEach(child => child.SetActive(active));
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
            elapsedTime += Time.unscaledDeltaTime;
        }

        SetAlpha(stop);

        then?.Invoke();


    }

    public void FadeOut(float duration, Action then = null)
    {
        try
        {
            StartCoroutine(FadeCoroutine(duration, 0, () =>
            {
                then.Invoke();
                ActivateChildren(false);
            }));
        }
        catch
        {
            //
        }
    }

    public void FadeIn(float duration, Action then = null)
    {
        ActivateChildren(true);
        StartCoroutine(FadeCoroutine(duration, 1, then));
    }
}
