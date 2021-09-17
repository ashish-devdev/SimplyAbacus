using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region CountDownTimer 

    /*
    public static float currentTime = 60;
    float t = 0;
    public float totalTime = 3600;
    public TextMeshProUGUI timeText;
    private void OnEnable()
    {
        t = 0;
        currentTime = totalTime;
    }
    private void Update()
    {
        if (currentTime > 0)
        {
            t += Time.deltaTime;
            currentTime = (totalTime - t);
            timeText.text = (currentTime).ToString("F1");
        }
        else
        {
            timeText.text = (0.00).ToString("F2");

        }
    }
    private void OnDisable()
    {
        currentTime = totalTime;
    }
    */
    #endregion

    #region CountUp Timer
    public static float currentTime = 60;
    public float startingTime = 0;
    public  static float  savedTime=0;
    public static TextMeshProUGUI timerText;
    TimeSpan ConvertedTime;

    private void OnEnable()
    {
        startingTime = 0;
        currentTime = savedTime;
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        startingTime+= Time.deltaTime;
        currentTime = savedTime + startingTime;
        ConvertedTime = TimeSpan.FromSeconds(currentTime);
        if (currentTime > 3600)
            timerText.text = ConvertedTime.ToString(@"hh\:mm\:ss");
        else
        {
            timerText.text = ConvertedTime.ToString(@"mm\:ss");
        }
    }

    public void ResetTime()
    {
        startingTime = 0;
        savedTime = 0;
    }
    #endregion

}
