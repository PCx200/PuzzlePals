using UnityEngine;

public class BedTeleport : MonoBehaviour, IInteractable
{
    [SerializeField] private BedTeleport linkedBed;

    protected BedTeleport LinkedBed;

    [SerializeField] private BoxCollider area;

    private void OnValidate()
    {
        if (area == null)
        {
            area = GetComponent<BoxCollider>();
        }
    }

    private void Start()
    {
        linkedBed.LinkedBed = this;
    }

    public void Interact()
    {
        var player = FindAnyObjectByType<PlayerController>();


        if (player == null)
        {
            Debug.Log("Player is null");
            return;
        }

        if (player.currentMonster.Name != MonsterCharacter.MonsterName.Home) return;

        Teleport(player);
    }
    private void Teleport(PlayerController player)
    {
        player.transform.position = linkedBed.area.transform.position;
    }

    private void OnDrawGizmos()
    {
        OnValidate();

        Gizmos.color = Color.darkBlue;
        Gizmos.DrawWireCube(area.transform.position, area.size);

        Gizmos.DrawLine(transform.position, linkedBed.transform.position);
    }
}
