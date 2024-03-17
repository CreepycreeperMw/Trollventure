using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour {
    [SerializeField] private Text text;

    private int frameIndex = 0;
    private float[] frameTimes = new float[100];
    float totalDeltaTime = 0;

    private DateTime lastUpdate = DateTime.Now;
    private float frequency = 20f;

    private void Update()
    {
        totalDeltaTime += Time.deltaTime;
        totalDeltaTime -= frameTimes[frameIndex];
        frameTimes[frameIndex] = Time.deltaTime;

        frameIndex++;
        if(frameIndex >= frameTimes.Length)
        {
            frameIndex = 0;
        }
        if(DateTime.Now.Subtract(lastUpdate).TotalMilliseconds > frequency)
        {
            text.text = "FPS: " + (int) (1 / (totalDeltaTime / 100));
            lastUpdate = DateTime.Now;
        }
    }
}
