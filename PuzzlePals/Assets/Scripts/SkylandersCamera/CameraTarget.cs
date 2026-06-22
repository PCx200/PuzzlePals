using UnityEngine;

/// <summary>
/// Place on an empty child of the player at chest/head height.
/// The camera director follows and looks at this transform, not the player root.
/// </summary>
public class CameraTarget : MonoBehaviour
{
    [SerializeField] private float gizmoRadius = 0.15f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
    }
}
