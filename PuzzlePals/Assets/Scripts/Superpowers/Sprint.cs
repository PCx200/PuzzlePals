using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sprint : SuperPower
{
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ParticleSystem particleEffects;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public override void SuperPowerPressed()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.sprint, transform.position);
        playerController.isSprinting = true;
        particleEffects.Play();
        playerController.currentMonster.Animator.PlaySuperPower("Sprint");
    }

    public override void SuperPowerReleased()
    {
        playerController.isSprinting = false;
        particleEffects.Stop();
    }
}
