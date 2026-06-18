using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TransformationPoint : MonoBehaviour, IInteractable
{
    public enum TransformationType { Instant, WithInteraction }

    [SerializeField] private TransformationType transformationType;

    [SerializeField] private MonsterCharacter characterToTransformInto;
    [SerializeField] BoxCollider area;

    [SerializeField] private List<MonsterCharacter> monsters = new List<MonsterCharacter>(); 

    public void Interact()
    {
        var player = FindAnyObjectByType<PlayerController>();

        if (player == null)
        {
            Debug.Log("Player is null");
            return;
        }

        if (player.currentMonster.Name == characterToTransformInto.Name)
        {
            Debug.Log("Trying to transform to the same monster");
            return;
        }

        Transform(player);
    }

    private void Transform(PlayerController player)
    {
        // why is 
        var currentMonster = monsters.Find(monster => monster.Name == player.currentMonster.Name);

        var prefabToSpawn = monsters.Find(m => m.Name == characterToTransformInto.Name);

        var newMonster = Instantiate(prefabToSpawn, player.transform.position, Quaternion.identity);

        Destroy(player.GetComponentInChildren<MonsterCharacter>().gameObject);

        newMonster.transform.SetParent(player.transform);

        characterToTransformInto = currentMonster;

        player.currentMonster = newMonster;
        
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.transformation, transform.position);
    }

    private void OnDrawGizmos()
    {
        ChangeAreaColor();
        Gizmos.DrawWireCube(transform.position, area.size);
    }

    private void ChangeAreaColor()
    {
        switch (characterToTransformInto.Name)
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
