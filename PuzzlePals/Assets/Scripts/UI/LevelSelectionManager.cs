using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelSelectionManager : MonoBehaviour
{
    private GameObject openPanel;
    
    [SerializeField] private EventSystem eventSystem;
    
    [SerializeField] private InputActionAsset asset;
    private InputAction goBack;
    private void Awake()
    {
        goBack = asset.FindAction("Cancel");

        
    }

    private void Start()
    {
        eventSystem = FindFirstObjectByType<EventSystem>();
        goBack.performed += ClosePanelInput;
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

    private void OnDisable()
    {
        asset.Disable();
        goBack.performed -= ClosePanelInput;
    }

    public void OpenPanel(GameObject levelDataPanel)
    {
        if (openPanel != null) openPanel.SetActive(false);
        
        levelDataPanel.SetActive(true);
        openPanel = levelDataPanel;
    }

    public void ClosePanelInput(InputAction.CallbackContext context)
    {
        if (openPanel != null)
        {
            openPanel.SetActive(false);
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
            openPanel = null;
        }
        else 
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    public void ClosePanel()
    {
        if (openPanel != null)
        {
            openPanel.SetActive(false);
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
            openPanel = null;
        }
    }
    
}
