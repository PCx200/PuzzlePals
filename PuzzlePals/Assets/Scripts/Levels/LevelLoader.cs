using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //[SerializeField] private float transitionTime = 1.5f;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private InputActionAsset asset;
    [SerializeField] private bool isOnLevelSelectionScene;
    private InputAction goBack;

    private void Awake()
    {
        goBack = asset.FindAction("Cancel");
    }
    public void Start()
    {
        goBack.performed += GoBackOnMenu;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        asset.Enable();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        asset.Enable();
    }

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
    private IEnumerator LoadLevelTransition(int buildIndex)
    {
        transitionAnimator.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transitionAnimator.GetCurrentAnimatorClipInfo(0).Length);

        SceneManager.LoadScene(buildIndex);
    }
    public void ReplayLevel()
    {
        StartCoroutine(LoadLevelTransition(SceneManager.GetActiveScene().name));
    }

    public void NextLevel()
    {
        var buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadLevelTransition(buildIndex));
    }

    public void GoBackOnMenu(InputAction.CallbackContext context)
    {
        if (isOnLevelSelectionScene)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void OnDisable()
    {
        goBack.performed -= GoBackOnMenu;
        asset.Disable();
    }
}
