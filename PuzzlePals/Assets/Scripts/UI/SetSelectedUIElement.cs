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
            SelectElement(toBeSelected);
        }
        
    }

    public void SetToBeSelected(GameObject go)
    {
        toBeSelected = go;
    }

    public void SelectElement(GameObject element)
    {
        if (eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(element);
        }
    }
}
