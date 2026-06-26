using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    bool isPaused;
    [SerializeField] GameObject pausePanel;

    private InputManager inputManager;
    public Action PauseEvent; // pause audio
    public Action UnPauseEvent; // un pause audio

    private void Start()
    {
        inputManager = InputManager.Instance;
        inputManager.PauseMenuAction.performed += PauseGame;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDestroy()
    {
        if (inputManager != null)
            inputManager.PauseMenuAction.performed -= PauseGame;
        
        Time.timeScale = 1.0f;
    }


    public void PauseGame(InputAction.CallbackContext ctx)
    {
        if (!isPaused)
        {
            Time.timeScale = 0.0f;
            pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            PauseEvent?.Invoke();
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            UnPauseEvent?.Invoke();
        }
        isPaused = !isPaused;
    }
    public void PauseGameButton()
    {
        if (!isPaused)
        {
            Time.timeScale = 0.0f;
            pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            PauseEvent?.Invoke();
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            UnPauseEvent?.Invoke();
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
