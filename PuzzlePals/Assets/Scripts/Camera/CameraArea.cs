using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraArea : MonoBehaviour
{
    private static bool transitioning = false;
    private static CinemachineFollow follow;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float blendTime;

    private void Start()
    {
        if (follow == null)
        {
            follow = FindFirstObjectByType<CinemachineFollow>();
        }
    }
    private void OnDisable()
    {
        transitioning = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Entered CameraArea Trigger");
            if (follow != null)
                StartCoroutine(LerpCamera());
            else Debug.LogWarning($"Cinemachine follow not found in: {gameObject.name}");
        }
    }

    private IEnumerator LerpCamera()
    {
        while (transitioning)
        {
            yield return null;
        }
        transitioning = true;
        Vector3 startOffset = new Vector3(follow.FollowOffset.x, follow.FollowOffset.y, follow.FollowOffset.z);
        float timer = 0f;
        while (timer <= blendTime)
        {
            timer += Time.deltaTime;
            follow.FollowOffset.x = Mathf.Lerp(startOffset.x, offset.x, timer / blendTime);
            follow.FollowOffset.y = Mathf.Lerp(startOffset.y, offset.y, timer / blendTime);
            follow.FollowOffset.z = Mathf.Lerp(startOffset.z, offset.z, timer / blendTime);
            yield return null;
        }
        transitioning = false;
    }
}
