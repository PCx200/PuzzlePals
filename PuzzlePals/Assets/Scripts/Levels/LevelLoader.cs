using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //[SerializeField] private float transitionTime = 1.5f;
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

        yield return new WaitForSecondsRealtime(transitionAnimator.GetCurrentAnimatorClipInfo(0).Length);

        SceneManager.LoadScene(sceneName);
    }
    public void ReplayLevel()
    {
        StartCoroutine(LoadLevelTransition(SceneManager.GetActiveScene().name));
    }

    public void NextLevel(string sceneName)
    {
        StartCoroutine(LoadLevelTransition(sceneName));
    }
}
