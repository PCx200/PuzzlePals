using UnityEngine;

public class BedTeleport : MonoBehaviour, IInteractable
{
    [SerializeField] private BedTeleport linkedBed;

    [SerializeField] private BoxCollider area;

    private void OnValidate()
    {
        if (area == null)
        {
            area = GetComponent<BoxCollider>();
        }
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
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.pipe, transform.position);
    }

    private void OnDrawGizmos()
    {
        OnValidate();

        Gizmos.color = Color.darkBlue;
        Gizmos.DrawWireCube(area.transform.position, area.size);

        Gizmos.DrawLine(transform.position, linkedBed.transform.position);
    }
}
