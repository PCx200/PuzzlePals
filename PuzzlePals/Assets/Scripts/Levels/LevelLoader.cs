using UnityEditor;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelData level;

    public void LoadLevel()
    {
        LevelManager.Instance.LoadLevel(level.sceneName);
    }
}
