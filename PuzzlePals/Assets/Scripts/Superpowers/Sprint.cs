using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sprint : SuperPower
{
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    public override void SuperPowerPressed()
    {
        playerController.isSprinting = true;
    }

    public override void SuperPowerReleased()
    {
        playerController.isSprinting = false;
    }
}
