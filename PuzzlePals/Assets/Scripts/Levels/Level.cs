using EventBus;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelData levelData;

    public LevelData LevelData => levelData;

    [SerializeField] private float completionTime; // the time since the start of the level.
    public float CompletionTime => completionTime;


    [SerializeField] private EndPoint endPoint;
    [SerializeField] private bool completed;

    
    public byte StarsEarnedThisRun { get; private set; }
    public event Action OnStarsCalculated;


    private void OnValidate()
    {
        if (levelData != null)
        {
            name = levelData.sceneName;
        }
    }

    private void OnEnable()
    {
        if (endPoint != null) endPoint.OnLevelCompleted += OnLevelCompleted;
        else Debug.LogWarning($"Level endpoint is null on {name}");
    }

    private void OnDisable()
    {
        if (endPoint != null) endPoint.OnLevelCompleted -= OnLevelCompleted;
        else Debug.LogWarning($"Level endpoint is null on {name}");
    }

    void Update()
    {
        if (!completed)
        {
            completionTime += Time.deltaTime;
        }
    }

    private void OnLevelCompleted()
    {

        if (levelData == null)
            return;

        completed = true;

        byte starsEarned = 1;
        var timeForStars = levelData.timeForStars;

        if (timeForStars.Count == 2)
        {
            if (completionTime <= timeForStars[0])
                starsEarned = 3;
            else if (completionTime <= timeForStars[1])
                starsEarned = 2;
        }

        StarsEarnedThisRun = starsEarned;
        OnStarsCalculated?.Invoke();

        LevelManager.Instance.UpdateLevelProgress(
            levelData.sceneName,
            (ushort)completionTime,
            starsEarned
        );
    }
}
