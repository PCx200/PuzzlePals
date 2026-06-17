using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HappinessDetector : MonoBehaviour
{
    [SerializeField] private LayerMask sadObjectLayer;
    [SerializeField] private Material happyMaterial;
    [SerializeField] private UnityEvent onAllHappy;

    private readonly List<MeshRenderer> sadObjects = new();
    private bool hasTriggered;

    private void Start()
    {
        foreach (var renderer in FindObjectsByType<MeshRenderer>(FindObjectsSortMode.None))
        {
            if (((1 << renderer.gameObject.layer) & sadObjectLayer) != 0)
                sadObjects.Add(renderer);
        }
    }

    private void Update()
    {
        if (hasTriggered || sadObjects.Count == 0)
            return;

        foreach (var renderer in sadObjects)
        {
            if (renderer == null || renderer.sharedMaterial != happyMaterial)
                return;
        }

        hasTriggered = true;
        onAllHappy.Invoke();
    }
}
