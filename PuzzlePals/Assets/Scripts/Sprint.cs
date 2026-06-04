using System.Runtime.CompilerServices;
using UnityEngine;

public class Sprint : SuperPower
{
    [SerializeField] private float sprintMultiplier;

    public override void UseSuperPower()
    {
        var player = FindAnyObjectByType<PlayerController>();

        var currentSpeed = player.currentMonster.Stats.movementSpeed;

        player.currentMonster.Stats.movementSpeed *= sprintMultiplier;
    }
}
