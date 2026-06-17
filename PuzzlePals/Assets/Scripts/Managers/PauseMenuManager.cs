using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    bool isPaused;
    [SerializeField] GameObject pausePanel;

    private InputManager inputManager;

    private void Start()
    {
        inputManager = InputManager.Instance;
        inputManager.PauseMenuAction.performed += PauseGame;
    }

    private void OnDestroy()
    {
        if (inputManager != null)
            inputManager.PauseMenuAction.performed -= PauseGame;
    }


    public void PauseGame(InputAction.CallbackContext ctx)
    {
        if (!isPaused)
        {
            Time.timeScale = 0.0f;
            pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        isPaused = !isPaused;
    }
    public void Restart()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
