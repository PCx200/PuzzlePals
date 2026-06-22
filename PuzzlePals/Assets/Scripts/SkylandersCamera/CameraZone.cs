using UnityEngine;

/// <summary>
/// Trigger volume that tells the camera director which framing to use.
/// Offsets are world-space (not relative to player facing).
/// </summary>
[RequireComponent(typeof(Collider))]
public class CameraZone : MonoBehaviour
{
    [Header("Director")]
    [SerializeField] private SkylandersCamera cameraDirector;
    [SerializeField] private string playerTag = "Player";

    [Header("Framing")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 7f, -9f);
    [SerializeField] private Vector3 lookOffset = new Vector3(0f, 1.5f, 0f);
    [SerializeField] private float fov = 45f;

    [Header("Transition")]
    [SerializeField] private float blendSpeed = 3f;
    [SerializeField] private int priority;

    public Vector3 Offset => offset;
    public Vector3 LookOffset => lookOffset;
    public float Fov => fov;
    public float BlendSpeed => blendSpeed;
    public int Priority => priority;

    private void Awake()
    {
        Collider zoneCollider = GetComponent<Collider>();
        zoneCollider.isTrigger = true;

        if (cameraDirector == null)
            cameraDirector = FindFirstObjectByType<SkylandersCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag) || cameraDirector == null)
            return;

        cameraDirector.EnterZone(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag) || cameraDirector == null)
            return;

        cameraDirector.ExitZone(this);
    }

    private void OnDrawGizmosSelected()
    {
        Collider zoneCollider = GetComponent<Collider>();
        if (zoneCollider == null)
            return;

        Gizmos.color = new Color(0.2f, 0.8f, 1f, 0.25f);
        Gizmos.matrix = transform.localToWorldMatrix;

        if (zoneCollider is BoxCollider box)
            Gizmos.DrawCube(box.center, box.size);
        else if (zoneCollider is SphereCollider sphere)
            Gizmos.DrawSphere(sphere.center, sphere.radius);
    }
}
