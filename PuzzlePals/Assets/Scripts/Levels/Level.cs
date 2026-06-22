using EventBus;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelData levelData;

    public LevelData LevelData => levelData;

    [SerializeField] private float completionTime; // the time since the start of the level.
    public float CompletionTime => completionTime;


    [SerializeField] private EndPoint endPoint;
    private bool completed;

    private void OnValidate()
    {
        if (levelData != null)
        {
            name = levelData.scene.name;
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

        if (completionTime < levelData.bestCompletionTime)
            levelData.bestCompletionTime = (ushort)completionTime;

        var timeForStars = levelData.timeForStars;

        if (timeForStars.Count == 2)
        {
            if (completionTime <= timeForStars[0])
                levelData.stars = 3;
            else if (completionTime <= timeForStars[1])
                levelData.stars = 2;
            else
                levelData.stars = 1;
        }

        if (LevelManager.Instance != null)
        { 
            LevelManager.Instance.OnLevelCompleted();
        }
    }
}
