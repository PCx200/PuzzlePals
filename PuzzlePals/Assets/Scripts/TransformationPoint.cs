using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TransformationPoint : MonoBehaviour, IInteractable
{
    public enum TransformationType { Instant, WithInteraction }

    [SerializeField] private TransformationType transformationType;

    [SerializeField] private MonsterCharacter characterToTransform;
    [SerializeField] BoxCollider area;

    //private void Awake()
    //{
    //    switch (transformationType)
    //    {
    //        case TransformationType.Instant:

    //            break;
    //        case TransformationType.WithInteraction:
    //            break;
    //        default:
    //            break;
    //    }
    //}

    public void Interact()
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
        var currentMonster = player.currentMonster;

        var swappedMonster = currentMonster;

        player.currentMonster = characterToTransform;


        var newMonster = Instantiate(player.currentMonster.Prefab, player.transform.position, Quaternion.identity);
        Destroy(player.GetComponentInChildren<MonsterCharacter>().gameObject);
        newMonster.transform.SetParent(player.transform);

        characterToTransform = swappedMonster;

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

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();

        if (player != null && transformationType == TransformationType.Instant)
        {
            Transform(player);
        }
    }
}
