using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TransformationPoint : MonoBehaviour, IInteractable
{
    public enum TransformationType { Instant, WithInteraction }

    [SerializeField] private TransformationType transformationType;

    [SerializeField] private MonsterCharacter characterToTransformInto;

    //private MonsterCharacter previousMonster;

    [SerializeField] BoxCollider area;

    private bool transformed = false;

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
            Debug.Log("Transformed into previous monster " + player.previousMonster.name);
            Transform(player, player.previousMonster);
        }
        else Transform(player, characterToTransformInto);
    }

    private void Transform(PlayerController player, MonsterCharacter transformInto)
    {
        player.previousMonster = player.currentMonster;
        
        player.currentMonster.gameObject.SetActive(false);

        var newCurrentMonster = player.monsters.Find(m => m.Name == transformInto.Name);

        newCurrentMonster.gameObject.SetActive(true);

        player.currentMonster = newCurrentMonster;

        Debug.Log("Transformed into " +  newCurrentMonster.Name);
        
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

        if (player == null || transformationType != TransformationType.Instant)
            return;

        if (!transformed)
        {
            player.previousMonster = player.currentMonster;
            Transform(player, characterToTransformInto);
            transformed = true;
        }
        else
        {
            Transform(player, player.previousMonster);
            transformed = false;
        }
    }
}
