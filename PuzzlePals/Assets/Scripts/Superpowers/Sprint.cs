using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sprint : SuperPower
{
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject particleEffects;

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    public override void SuperPowerPressed()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.sprint, transform.position);
        playerController.isSprinting = true;
        particleEffects.SetActive(true);
    }

    public override void SuperPowerReleased()
    {
        playerController.isSprinting = false;
        particleEffects.SetActive(false);
    }
}
