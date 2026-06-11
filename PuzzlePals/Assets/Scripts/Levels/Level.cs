using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelData levelData;

    [SerializeField] private float timeElapsed; // the time since the start of the level.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed = Time.time;
    }

    public void OnLevelEnd()
    {
        // 3 - 30 secs
        // 2 - 45 secs
        // 1 - 60 secs

        if (levelData == null)
        {
            return;
        }

        var timeForStars = levelData.timeForStars;

        if (timeForStars.Count != 3)
        {
            return;
        }

        if (timeElapsed < timeForStars[0])
        {
            levelData.stars = 3;
        }
        else if (timeElapsed < timeForStars[1])
        {
            levelData.stars = 2;
        }
        else
        {
            levelData.stars = 1;
        }
    }
}
