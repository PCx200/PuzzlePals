using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TransformationPoint : MonoBehaviour
{
    public enum TransformationType { Instant, WithInteraction }

    [SerializeField] private TransformationType transformationType;
    [SerializeField] private MonsterCharacter characterToTransformInto;

    //private MonsterCharacter previousMonster;

    [SerializeField] BoxCollider area;

    [SerializeField] private Transform tentLight;

    [SerializeField] private Color[] tentLightColors;

    private void Awake()
    {
        if (tentLight != null)
        {
            var renderer = tentLight.GetComponent<Renderer>();

            // Force a unique material instance for THIS tent
            renderer.material = new Material(renderer.sharedMaterial);

            ChangeLightMaterialColor();
        }
    }


    private void Transform(PlayerController player, MonsterCharacter transformInto)
    {
        player.currentMonster.gameObject.SetActive(false);

        var newCurrentMonster = player.monsters.Find(m => m.Name == transformInto.Name);

        newCurrentMonster.gameObject.SetActive(true);
        
        characterToTransformInto = player.currentMonster;

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

    private void ChangeLightMaterialColor()
    {
        var material = tentLight.GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");

        switch (characterToTransformInto.Name)
        {
            case MonsterCharacter.MonsterName.Mida:
                material.SetColor("_EmissionColor", tentLightColors[0]);
                break;
            case MonsterCharacter.MonsterName.Copkac:
                material.SetColor("_EmissionColor", tentLightColors[1]);
                break;
            case MonsterCharacter.MonsterName.Home:
                material.SetColor("_EmissionColor", tentLightColors[2]);
                break;
            case MonsterCharacter.MonsterName.Jullia:
                material.SetColor("_EmissionColor", tentLightColors[3]);
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
        
        Transform(player, characterToTransformInto);
        ChangeLightMaterialColor();
    }
}
