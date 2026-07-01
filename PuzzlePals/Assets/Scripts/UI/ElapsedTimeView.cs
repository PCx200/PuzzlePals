using System;
using TMPro;
using UnityEngine;

public class ElapsedTimeView : MonoBehaviour
{
    private Level level;
    private EndPoint endPoint;

    [SerializeField] private TextMeshProUGUI timeElapsedText;

    private int totalSeconds;
    private int minutes;
    private int seconds;

    private void Awake()
    {
        level = FindFirstObjectByType<Level>();
        endPoint = FindFirstObjectByType<EndPoint>();
    }

    private void OnEnable()
    {
        if (endPoint != null)
        {
            endPoint.OnLevelCompleted += DisableView ;
        }
    }

    private void OnDisable()
    {
        if (endPoint != null)
        {
            endPoint.OnLevelCompleted -= DisableView;
        }
    }

    void Update()
    {
        if (level != null)
        {
            totalSeconds = (int)level.CompletionTime;
            minutes = totalSeconds / 60;
            seconds = totalSeconds % 60;

            timeElapsedText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    private void DisableView()
    {
        gameObject.SetActive(false);
    }
}
