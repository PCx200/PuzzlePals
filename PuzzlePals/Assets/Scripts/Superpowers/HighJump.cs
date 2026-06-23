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
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.copkacJump, transform.position);
        player.SuperJump(jumpHeight);
        highJump.Play();
        Debug.Log("Jumped with height 8");
    }
}
