using UnityEngine;

public class CheckPointHandler : MonoBehaviour
{
    [HideInInspector] public Vector3 currentCheckpoint;
    
    private void TeleportToCheckpoint()
    {
            transform.position = currentCheckpoint;
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.returnSpawnpoint, transform.position);
    }

    private void Start()
    {
        currentCheckpoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    private void FixedUpdate()
    {
        if (transform.position.y < -15f)
        {
            TeleportToCheckpoint();
        }
    }
}
