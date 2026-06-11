using UnityEditor;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private SceneAsset levelToLoad;

    public void LoadLevel()
    {
        LevelManager.Instance.LoadLevel(levelToLoad);
    }
}
