using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Director-driven cinematic camera. Follows a CameraTarget at a fixed world-space offset.
/// Only offset, look offset, and FOV blend when camera zones change — never the rig position.
/// </summary>
[DefaultExecutionOrder(1000)]
public class SkylandersCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform followTarget;
    [SerializeField] private Camera cam;

    [Header("Default Settings (used when no zone is active)")]
    [SerializeField] private Vector3 defaultOffset = new Vector3(0f, 7f, -9f);
    [SerializeField] private Vector3 defaultLookOffset = new Vector3(0f, 1.5f, 0f);
    [SerializeField] private float defaultFov = 45f;
    [SerializeField] private float defaultBlendSpeed = 3f;

    [Header("Setup")]
    [SerializeField] private bool disableCompetingCinemachine = true;
    [SerializeField] private bool snapOnPlay = true;

    private Vector3 currentOffset;
    private Vector3 targetOffset;
    private Vector3 currentLookOffset;
    private Vector3 targetLookOffset;
    private float currentFov;
    private float targetFov;
    private float activeBlendSpeed;

    private readonly HashSet<CameraZone> activeZones = new HashSet<CameraZone>();
    private bool warnedMissingTarget;

    private void Awake()
    {
        ResolveReferences();
        if (disableCompetingCinemachine)
            DisableCompetingCinemachine();

        ResetToDefaults(immediate: true);
    }

    private void Start()
    {
        ResolveReferences();

        if (snapOnPlay)
            ApplyCameraTransform();
    }

    private void LateUpdate()
    {
        if (!EnsureReady())
            return;

        BlendZoneSettings();
        ApplyCameraTransform();
    }

    private bool EnsureReady()
    {
        if (followTarget == null || cam == null)
            ResolveReferences();

        if (followTarget == null)
        {
            if (!warnedMissingTarget)
            {
                Debug.LogWarning(
                    "SkylandersCamera: No follow target found. Add a CameraTarget child to the Player " +
                    "and assign it, or tag the Player as 'Player' so it can be found automatically.",
                    this);
                warnedMissingTarget = true;
            }

            return false;
        }

        if (cam == null)
        {
            Debug.LogWarning("SkylandersCamera: No Camera assigned or found.", this);
            return false;
        }

        return true;
    }

    private void ResolveReferences()
    {
        if (cam == null)
        {
            cam = GetComponentInChildren<Camera>();

            if (cam == null && Camera.main != null)
                cam = Camera.main;
        }

        if (followTarget != null)
            return;

        CameraTarget targetComponent = FindFirstObjectByType<CameraTarget>();
        if (targetComponent != null)
        {
            followTarget = targetComponent.transform;
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            return;

        Transform namedTarget = player.transform.Find("CameraTarget");
        if (namedTarget != null)
        {
            followTarget = namedTarget;
            return;
        }

        followTarget = player.transform;
        Debug.LogWarning(
            "SkylandersCamera: No CameraTarget found on Player — using the Player root. " +
            "Add an empty child named 'CameraTarget' at chest height for better framing.",
            this);
    }

    private void ApplyCameraTransform()
    {
        Vector3 anchor = followTarget.position;
        Vector3 worldPosition = anchor + currentOffset;
        Vector3 lookPoint = anchor + currentLookOffset;
        Vector3 lookDirection = lookPoint - worldPosition;

        Quaternion worldRotation = lookDirection.sqrMagnitude > 0.0001f
            ? Quaternion.LookRotation(lookDirection, Vector3.up)
            : cam.transform.rotation;

        // Drive the actual camera transform so this works even when the script
        // is not on the same object as the Camera (e.g. rig root + assigned Main Camera).
        cam.transform.SetPositionAndRotation(worldPosition, worldRotation);
        cam.fieldOfView = currentFov;

        // If this script lives on a separate rig root, keep that in sync too.
        if (transform != cam.transform)
            transform.SetPositionAndRotation(worldPosition, worldRotation);
    }

    private void BlendZoneSettings()
    {
        if (Approximately(currentOffset, targetOffset) &&
            Approximately(currentLookOffset, targetLookOffset) &&
            Mathf.Approximately(currentFov, targetFov))
        {
            return;
        }

        float blendStep = Time.deltaTime * activeBlendSpeed;

        currentOffset = Vector3.Lerp(currentOffset, targetOffset, blendStep);
        currentLookOffset = Vector3.Lerp(currentLookOffset, targetLookOffset, blendStep);
        currentFov = Mathf.Lerp(currentFov, targetFov, blendStep);

        if (Vector3.Distance(currentOffset, targetOffset) < 0.001f)
            currentOffset = targetOffset;
        if (Vector3.Distance(currentLookOffset, targetLookOffset) < 0.001f)
            currentLookOffset = targetLookOffset;
        if (Mathf.Abs(currentFov - targetFov) < 0.01f)
            currentFov = targetFov;
    }

    private static bool Approximately(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b) < 0.001f;
    }

    private void DisableCompetingCinemachine()
    {
        MonoBehaviour[] behaviours = FindObjectsByType<MonoBehaviour>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None);

        foreach (MonoBehaviour behaviour in behaviours)
        {
            if (behaviour == null)
                continue;

            string typeName = behaviour.GetType().FullName;
            if (typeName != null && typeName.StartsWith("Unity.Cinemachine."))
                behaviour.enabled = false;
        }
    }

    public void EnterZone(CameraZone zone)
    {
        if (zone == null)
            return;

        activeZones.Add(zone);
        ApplyHighestPriorityZone();
    }

    public void ExitZone(CameraZone zone)
    {
        if (zone == null)
            return;

        activeZones.Remove(zone);
        ApplyHighestPriorityZone();
    }

    private void ApplyHighestPriorityZone()
    {
        CameraZone bestZone = null;
        int bestPriority = int.MinValue;

        foreach (CameraZone zone in activeZones)
        {
            if (zone.Priority > bestPriority)
            {
                bestPriority = zone.Priority;
                bestZone = zone;
            }
        }

        if (bestZone != null)
            ApplyZoneSettings(bestZone);
        else
            ResetToDefaults(immediate: false);
    }

    private void ApplyZoneSettings(CameraZone zone)
    {
        targetOffset = zone.Offset;
        targetLookOffset = zone.LookOffset;
        targetFov = zone.Fov;
        activeBlendSpeed = zone.BlendSpeed;
    }

    private void ResetToDefaults(bool immediate)
    {
        targetOffset = defaultOffset;
        targetLookOffset = defaultLookOffset;
        targetFov = defaultFov;
        activeBlendSpeed = defaultBlendSpeed;

        if (immediate)
        {
            currentOffset = targetOffset;
            currentLookOffset = targetLookOffset;
            currentFov = targetFov;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (defaultBlendSpeed < 0.01f)
            defaultBlendSpeed = 0.01f;
    }

    private void OnDrawGizmosSelected()
    {
        Transform target = followTarget;
        if (target == null)
        {
            CameraTarget found = FindFirstObjectByType<CameraTarget>();
            if (found != null)
                target = found.transform;
        }

        if (target == null)
            return;

        Vector3 anchor = target.position;
        Vector3 cameraPos = anchor + defaultOffset;
        Vector3 lookPoint = anchor + defaultLookOffset;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(cameraPos, 0.35f);
        Gizmos.DrawLine(cameraPos, lookPoint);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(lookPoint, 0.2f);
    }
#endif
}
