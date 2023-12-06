using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CurrentTime : MonoBehaviour
{
    // Text UI 사용
    public Text timeText;

    // 또는 TMPro 사용
    public TextMeshProUGUI timeTextMeshPro;

    void Start()
    {
        // Invoke the UpdateTime method every second
        InvokeRepeating("UpdateTime", 0f, 1f);
    }

    void UpdateTime()
    {
        // Get the current system time
        System.DateTime currentTime = System.DateTime.Now;

        // Format the time and date as a string
        string timeString = currentTime.ToString("HH:mm:ss");
        string dateString = currentTime.ToString("yyyy년 MM월 dd일");

        // Update the UI text with the current time and date
        // Text UI 사용
        if (timeText != null)
            timeText.text = dateString + " " + timeString;

        // 또는 TMPro 사용
        if (timeTextMeshPro != null)
            timeTextMeshPro.text = dateString + " " + timeString;
    }
}
