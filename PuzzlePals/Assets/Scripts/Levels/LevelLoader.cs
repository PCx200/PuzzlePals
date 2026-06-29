using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float transitionTime = 1.5f;
    [SerializeField] private Animator transitionAnimator;

    public void ExitGame()
    { 
        Application.Quit();
    }

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadLevelTransition(sceneName));
    }
    private IEnumerator LoadLevelTransition(string sceneName)
    {
        transitionAnimator.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
    public void ReplayLevel()
    {
        string sceneName = SceneManager.GetActiveScene().ToString();

        StartCoroutine(LoadLevelTransition(sceneName));

    }

    public void NextLevel()
    {
        string sceneName = SceneManager.GetActiveScene() + 1.ToString();

        StartCoroutine(LoadLevelTransition(sceneName));
    }
}
