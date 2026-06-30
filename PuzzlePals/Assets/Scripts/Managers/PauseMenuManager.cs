using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    bool isPaused;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject completedPanel;

    private InputManager inputManager;
    public Action PauseEvent; // pause audio
    public Action UnPauseEvent; // un pause audio

    private void Start()
    {
        if (pausePanel != null) Debug.LogWarning("Forgot to assign pause panel on Canvas", this);
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
        if (pausePanel != null && !completedPanel.activeSelf)
        {
            if (!isPaused)
            {
                Time.timeScale = 0.0f;
                pausePanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                PauseEvent?.Invoke();
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                pausePanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                UnPauseEvent?.Invoke();
                isPaused = false;
            }
        }
    }
    public void PauseGameButton()
    {
        if (pausePanel != null)
        {
            if (!isPaused)
            {
                Time.timeScale = 0.0f;
                pausePanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                PauseEvent?.Invoke();
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                pausePanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                UnPauseEvent?.Invoke();
                isPaused = false;
            }
        }
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
