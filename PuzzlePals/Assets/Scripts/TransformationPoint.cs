using NUnit.Framework.Constraints;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

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

        player.currentMonster = characterToTransform;
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
