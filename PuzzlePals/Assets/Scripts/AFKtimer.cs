using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AFKtimer : MonoBehaviour
{
    [SerializeField] private float afkTime;
    [SerializeField] private float maxAfkTime;
    [SerializeField] private LevelLoader levelLoader;

    private void Reset()
    {
        levelLoader = FindFirstObjectByType<LevelLoader>();
        maxAfkTime = 300f;
    }

    private void OnEnable()
    {
        // Subscribe to the global action change event
        InputSystem.onActionChange += OnActionTriggered;
    }
    private void OnActionTriggered(object actionObject, InputActionChange changeType)
    {
        // We only care when an action is actively performed
        if (changeType != InputActionChange.ActionPerformed) return;

        afkTime = 0f;
    }
    private void Update()
    {
        afkTime += Time.deltaTime;
        if (afkTime > maxAfkTime)
        {
            levelLoader.LoadLevel(0);
        }
    }
}
