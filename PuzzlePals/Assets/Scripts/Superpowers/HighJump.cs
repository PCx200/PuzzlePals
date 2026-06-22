using UnityEngine;

public class HighJump : SuperPower
{
    private PlayerController player;
    [SerializeField] private float jumpHeight;
    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }
    public override void SuperPowerPressed()
    {
        player.SuperJump(jumpHeight);
        Debug.Log("Jumped with height 8");
    }
}
