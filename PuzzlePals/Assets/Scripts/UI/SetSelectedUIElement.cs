using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetSelectedUIElement : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject toBeSelected;

    private void Reset()
    {
        eventSystem = FindFirstObjectByType<EventSystem>();
        if (eventSystem == null)
        {
            Debug.LogWarning("No eventSystem found", this);
        }
    }

    private void Start()
    {
        eventSystem = FindFirstObjectByType<EventSystem>();
    }

    private void Update()
    {
        if (eventSystem.currentSelectedGameObject == null)
        {
            SelectElement();
        }
    }

    public void SelectElement()
    {
        if (eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(toBeSelected);
        }
    }
}
