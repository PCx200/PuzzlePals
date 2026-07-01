using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedView : MonoBehaviour
{
    private Level level;

    [SerializeField] private GameObject panel;

    [SerializeField] private TextMeshProUGUI completionTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private List<Image> stars;

    private void Awake()
    {
        level = FindFirstObjectByType<Level>();
    }

    private void OnEnable()
    {
        level.OnStarsCalculated += EnableLevelCompletedPanel;
        level.OnStarsCalculated += EnableStarsOnLevelCompletion;
    }

    private void OnDisable()
    {
        level.OnStarsCalculated -= EnableLevelCompletedPanel;
        level.OnStarsCalculated -= EnableStarsOnLevelCompletion;
    }

    private void EnableLevelCompletedPanel()
    {
        if (level == null)
        {
            Debug.LogWarning($"Level: {level} is null");
            return;
        }

        Cursor.lockState = CursorLockMode.None;
        panel.SetActive(true);

        completionTimeText.text = $"{FormatTime(level.CompletionTime)}";

        var entry = LevelManager.Instance.SaveData.GetOrCreate(level.LevelData.sceneName);

        ushort bestTime = entry.bestTime;

        if (bestTime == 0 || level.CompletionTime < bestTime)
            bestTime = (ushort)level.CompletionTime;

        bestTimeText.text = $"{FormatTime(bestTime)}";
        levelNameText.text = level.LevelData.levelName;
    }

    private void EnableStarsOnLevelCompletion()
    {
        StartCoroutine(EnableStars());
    }

    private IEnumerator EnableStars()
    {
        foreach (var star in stars)
        {
            star.gameObject.SetActive(false);
        }

        for (int i = 0; i < level.StarsEarnedThisRun; i++)
        {
            //using this because we stop the time when the level is completed
            yield return new WaitForSecondsRealtime(0.5f);
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.starAppear, transform.position);
            stars[i].gameObject.SetActive(true);   
        }
    }

    private string FormatTime(float time)
    {
        int minutes = (int)(time / 60f);
        int seconds = (int)(time % 60f);
        return $"{minutes:00}:{seconds:00}";
    }
}   
