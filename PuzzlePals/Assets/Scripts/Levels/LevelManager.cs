using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private List<LevelData> levels;
    public List<LevelData> AllLevels => levels;

    [SerializeField] private byte starsCollected;

    public byte StarsCollected => starsCollected;

    private SaveData saveData;

    public SaveData SaveData => saveData;

    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        saveData = SaveSystem.Load();
        ApplySaveToLevels();
        RecalculateStars();
    }

    private void ApplySaveToLevels()
    {
        foreach (var level in levels)
        {
            if (level == null)
            {
                Debug.LogError("LevelManager: A NULL LevelData was found in the list!");
                continue;
            }

            if (string.IsNullOrEmpty(level.sceneName))
            {
                Debug.LogError($"LevelData '{level.name}' has EMPTY sceneName!");
                continue;
            }

            if (saveData == null)
            {
                Debug.LogError("SaveData is NULL!");
                return;
            }

            var entry = saveData.GetOrCreate(level.sceneName);
        }
    }
    //gets the data from the savefile and checks if it beats the best time or not, then updates the stars and all the data and saves it back
    public void UpdateLevelProgress(string sceneName, ushort newTime, byte stars)
    {
        var entry = saveData.GetOrCreate(sceneName);

        if (entry.bestTime == 0 || newTime < entry.bestTime)
            entry.bestTime = newTime;

        if (stars > entry.stars)
            entry.stars = stars;

        SaveSystem.Save(saveData);
        RecalculateStars();
    }


    private void RecalculateStars()
    {
        starsCollected = 0;

        foreach (var entry in saveData.levels)
            starsCollected += entry.stars;
    }
}
