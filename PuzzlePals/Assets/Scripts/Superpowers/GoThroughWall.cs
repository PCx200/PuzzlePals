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
        gameObject.GetComponentInChildren<MeshRenderer>().material = opaqueMaterial;
    }
    public override void SuperPowerPressed()
    {
        if (!active)
        {
            player.gameObject.layer = LayerMask.NameToLayer("MIDA");
            active = true;
            gameObject.GetComponentInChildren<MeshRenderer>().material = transparentMaterial;
            if (IceWalls.instance != null)
                IceWalls.instance.TurnTransparent();
            else Debug.LogWarning($"IceWalls instance is null on {gameObject.name}");
            Debug.Log("Set the layer of the player to MIDA");
        }else
        {
            player.gameObject.layer = LayerMask.NameToLayer("Player");
            active= false;
            gameObject.GetComponentInChildren<MeshRenderer>().material = opaqueMaterial;
            if (IceWalls.instance != null)
                IceWalls.instance.TurnOpaque();
            else Debug.LogWarning($"IceWalls instance is null on {gameObject.name}");
            Debug.Log("Set the layer of the player to PLayer");
        }
    }
}
