using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    bool isPaused;
    Canvas canvas;

    private InputManager inputManager;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
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
            canvas.enabled = true;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            canvas.enabled = false;
            Cursor.visible = false;
        }
        isPaused = !isPaused;
    }
}
