using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    AudioSource music;
    float timeElapsed = 0;
    bool isFading = false;

    float fadeTime = 0;
    float delay = 0;

    float startVolume;
    float targetVolume = 0;
    Coroutine c = null;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        music = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isFading) return;
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= fadeTime)
        {
            music.volume = targetVolume;
            Debug.Log("DONE");
            StopFade();
            return;
        }

        float progress = timeElapsed / fadeTime;

        music.volume = targetVolume * progress + startVolume * (1 - progress);
    }


    public void Fade(float fadeTimeSec, float delaySeconds, float toVol)
    {
        Fade(fadeTimeSec, delaySeconds, music.volume, toVol);
    }

    public void Fade(float fadeTimeSec, float delaySeconds, float fromVol, float toVol)
    {
        fadeTime = fadeTimeSec;
        delay = delaySeconds;
        timeElapsed = 0;

        startVolume = fromVol;
        targetVolume = toVol;
        if (c != null) StopCoroutine(c);
        c = StartCoroutine(StartFade());
    }

    private IEnumerator StartFade()
    {
        yield return new WaitForSeconds(delay);
        isFading = true;
        Debug.Log("START");
    }
    public void StopFade()
    {
        isFading = false;
        timeElapsed = 0;
        StopCoroutine(c);
        Debug.Log("STOP");
    }


    // ////////////////////
    // TESTING STUFF
    // ////////////////////
    [ContextMenu("FadeOut")]
    void TestIn()
    {
        Fade(3, 0, 1, 0);
    }

    [ContextMenu("FadeIn")]
    public void TestOut()
    {
        Fade(fadeTime, 0, 0, 1);
    }
}
