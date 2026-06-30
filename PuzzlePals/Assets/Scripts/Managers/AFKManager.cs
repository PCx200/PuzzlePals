using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class AFKManager : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [Tooltip("Seconds of complete inactivity before returning to the main menu.")]
    [SerializeField] private float afkTimeout = 60f;

    // Ignore tiny device noise (e.g. resting controller stick / gyro jitter).
    [SerializeField] private float inputMagnitudeThreshold = 0.05f;

    private float idleTimer;

    private void OnEnable()
    {
        idleTimer = 0f;
        InputSystem.onEvent += OnInputEvent;
    }

    private void OnDisable()
    {
        InputSystem.onEvent -= OnInputEvent;
    }

    private void Update()
    {
        // Unscaled time so AFK still triggers while the game is paused (Time.timeScale == 0).
        idleTimer += Time.unscaledDeltaTime;

        if (idleTimer >= afkTimeout)
        {
            idleTimer = 0f;
            ReturnToMainMenu();
        }
    }

    private void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
    {
        // Only state/delta events carry control actuation.
        if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
            return;

        // Reset only when a control actually moved past the noise threshold.
        foreach (var _ in eventPtr.EnumerateChangedControls(device, inputMagnitudeThreshold))
        {
            idleTimer = 0f;
            return;
        }
    }

    private void ReturnToMainMenu()
    {
        if (SceneManager.GetActiveScene().name == mainMenuSceneName)
            return;

        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
