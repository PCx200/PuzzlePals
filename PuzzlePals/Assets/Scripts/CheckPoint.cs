using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform teleportationPoint;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Player"))
        {
            CheckPointHandler checkPointHandler = obj.GetComponent<CheckPointHandler>();
            if (checkPointHandler != null) checkPointHandler.currentCheckpoint = new Vector3(teleportationPoint.position.x, teleportationPoint.position.y, teleportationPoint.position.z);
        }
    }
}
