using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TransformationPoint : MonoBehaviour, IInteractable
{
    [SerializeField] private MonsterCharacter characterToTransform;
    [SerializeField] BoxCollider area;
    
    void IInteractable.Interact()
    {
        var player = FindAnyObjectByType<PlayerController>();

        if (player == null)
        {
            Debug.Log("Player is null");
            return;
        }

        if (player.currentMonster.Name == characterToTransform.Name)
        {
            Debug.Log("Trying to transform to the same monster");
            return;
        }

        Transform(player);
    }

    private void Transform(PlayerController player)
    {
        player.currentMonster = characterToTransform;
        var newMonster = Instantiate(player.currentMonster.Prefab, player.transform.position, Quaternion.identity);
        Destroy(player.GetComponentInChildren<MonsterCharacter>().gameObject);
        newMonster.transform.SetParent(player.transform);
        Debug.Log("Transformed into " + characterToTransform.Name);
    }

    private void OnDrawGizmos()
    {
        ChangeAreaColor();
        Gizmos.DrawWireCube(transform.position, area.size);
    }

    private void ChangeAreaColor()
    {
        switch (characterToTransform.Name)
        {
            case MonsterCharacter.MonsterName.Mida:
                Gizmos.color = Color.cyan;
                break;
            case MonsterCharacter.MonsterName.Copkac:
                Gizmos.color = Color.yellow;
                break;
            case MonsterCharacter.MonsterName.Home:
                Gizmos.color = Color.darkBlue;
                break;
            case MonsterCharacter.MonsterName.Jullia:
                Gizmos.color = Color.pink;
                break;
            default:
                break;
        }
    }
}
