using UnityEngine;
//the wall has to have the iceCreamWall layer assigned in order to work
public class GoThroughWall : SuperPower
{
    private PlayerController player;
    private bool active = false;
    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }
    public override void SuperPowerPressed()
    {
        if (!active)
        {
            player.gameObject.layer = LayerMask.NameToLayer("MIDA");
            active = true;
            Debug.Log("Set the layer of the player to MIDA");
        }else
        {
            player.gameObject.layer = LayerMask.NameToLayer("Player");
            active= false;
            Debug.Log("Set the layer of the player to PLayer");
        }
    }
}
