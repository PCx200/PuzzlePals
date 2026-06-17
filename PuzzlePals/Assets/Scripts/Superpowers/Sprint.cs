using System.Runtime.CompilerServices;
using UnityEngine;

public class Sprint : SuperPower
{
    [SerializeField] private float sprintMultiplier;

    public override void UseSuperPower()
    {
        var player = FindAnyObjectByType<PlayerController>();
        player.currentMonster.Stats.sprintMultiplier = sprintMultiplier;
    }
}
