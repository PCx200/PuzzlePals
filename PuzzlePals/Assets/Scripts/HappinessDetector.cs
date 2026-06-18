using UnityEngine;
using UnityEngine.Events;

public class HappinessDetector : MonoBehaviour
{
    [SerializeField] private LayerMask sadObjectLayer;
    [SerializeField] private Material happyMaterial;
    [SerializeField] private UnityEvent onAllHappy;

    private void Update()
    {
        bool foundAny = false;
        var renderers = FindObjectsByType<MeshRenderer>(FindObjectsSortMode.None);
        foreach (var renderer in renderers)
        {
            if (((1 << renderer.gameObject.layer) & sadObjectLayer) == 0)
                continue;

            foundAny = true;

            if (renderer.sharedMaterial != happyMaterial)
                return;
        }

        if (!foundAny)
            return;

        onAllHappy.Invoke();
        enabled = false;
    }
}
