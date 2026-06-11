using EventBus;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelData levelData;

    [SerializeField] private float timeElapsed; // the time since the start of the level.
    public float TimeElapsed => timeElapsed;


    [SerializeField] private EndPoint endPoint;

    private void OnValidate()
    {
        if (levelData != null)
        {
            name = levelData.scene.name;
        }
    }

    private void OnEnable()
    {
        endPoint.OnLevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        endPoint.OnLevelCompleted -= OnLevelCompleted;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    private void OnLevelCompleted()
    {

        if (levelData == null)
            return;

        if (timeElapsed < levelData.bestCompletionTime)
            levelData.bestCompletionTime = (ushort)timeElapsed;

        var timeForStars = levelData.timeForStars;

        if (timeForStars.Count == 2)
        {
            if (timeElapsed <= timeForStars[0])
                levelData.stars = 3;
            else if (timeElapsed <= timeForStars[1])
                levelData.stars = 2;
            else
                levelData.stars = 1;
        }

        LevelManager.Instance.OnLevelCompleted();
    }
}
