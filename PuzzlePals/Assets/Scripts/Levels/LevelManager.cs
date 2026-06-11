using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private List<LevelData> levels;
    public List<LevelData> AllLevels => levels;

    [SerializeField] private byte starsCollected;

    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        RecalculateStars();
    }

    public void LoadLevel(SceneAsset levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad.name);
    }

    public void OnLevelCompleted()
    {
        Debug.Log("bazinga");

        RecalculateStars();
        StartCoroutine(ReturnToMenu());
    }

    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("LevelsMenu");
    }

    private void RecalculateStars()
    {
        foreach (var level in levels)
        {
            starsCollected += level.stars;
        }
    }
}
