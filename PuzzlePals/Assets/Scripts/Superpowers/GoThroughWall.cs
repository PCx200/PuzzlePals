using UnityEngine;
//the wall has to have the iceCreamWall layer assigned in order to work
public class GoThroughWall : SuperPower
{
    private PlayerController player;
    private bool active = false;
    [SerializeField] private Material transparentMaterial;
    [SerializeField] private Material opaqueMaterial;
    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = opaqueMaterial;
    }
    public override void SuperPowerPressed()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ghostMode, transform.position);
        if (!active)
        {
            player.gameObject.layer = LayerMask.NameToLayer("MIDA");
            active = true;
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = transparentMaterial;
            IceWalls.instance.TurnTransparent();
            Debug.Log("Set the layer of the player to MIDA");
        }else
        {
            player.gameObject.layer = LayerMask.NameToLayer("Player");
            active= false;
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = opaqueMaterial;
            IceWalls.instance.TurnOpaque();
            Debug.Log("Set the layer of the player to Player");
        }
    }
}
