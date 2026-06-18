using UnityEngine;

public class BedTeleport : MonoBehaviour
{
    [SerializeField] private BedTeleport linkedBed;

    public BedTeleport LinkedBed => linkedBed;

    [SerializeField] private BoxCollider area;

    private void OnValidate()
    {
        if (area == null)
        {
            area = GetComponent<BoxCollider>();
        }
    }

    private void OnDrawGizmos()
    {
        if (linkedBed == null) return;
        OnValidate();

        Gizmos.color = Color.darkBlue;

        Gizmos.DrawLine(transform.position, linkedBed.transform.position);
    }
}