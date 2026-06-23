using UnityEngine;

public class HighJump : SuperPower
{
    private PlayerController player;
    [SerializeField] private float jumpHeight;
    [SerializeField] private ParticleSystem highJump;
    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }
    public override void SuperPowerPressed()
    {
        player.SuperJump(jumpHeight);
        highJump.Play();
        Debug.Log("Jumped with height 8");
    }
}
