using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float timeElapsed = 0f;
    bool isFading = false;

    float fadeTime = 0;
    float delay = 0;

    float _targetOpacity = 0;
    float _startOpacity;
    Coroutine c = null;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isFading) return;
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= fadeTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, _targetOpacity);
            StopFade();
            return;
        }

        float progress = timeElapsed / fadeTime;

        spriteRenderer.color = new Color(1, 1, 1, _targetOpacity*progress+_startOpacity*(1-progress));
    }

    [ContextMenu("StartFade")]
    void test()
    {
        StartFade(1);
    }
    public void StartFade(float fadeTimeSec)
    {
        StartFade(fadeTimeSec, 0, 0);
    }
    public void StartFade(float fadeTimeSec, int delayMs)
    {
        StartFade(fadeTimeSec, delayMs, 0);
    }
    public void StartFade(float fadeTimeSec, float delaySeconds, float targetOpacity)
    {
        StartFade(fadeTimeSec, delaySeconds, targetOpacity, spriteRenderer.color.a);
    }
    public void StartFade(float fadeTimeSec, float delaySeconds, float targetOpacity, float startOpacity)
    {
        timeElapsed = 0f;
        fadeTime = fadeTimeSec;
        delay = delaySeconds;

        _targetOpacity = targetOpacity;
        _startOpacity = startOpacity;
        if(c!=null) StopCoroutine(c);
        c = StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(delay);
        isFading = true;
    }

    [ContextMenu("StopFade")]
    public void StopFade()
    {
        isFading = false;
        StopCoroutine(c);
    }
}
