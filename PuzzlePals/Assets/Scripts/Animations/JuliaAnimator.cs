using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class JuliaAnimator : MonoBehaviour, IMonsterAnimator
{
    [SerializeField] private Animator animator;

    [SerializeField] private List<string> superPowerIndexName;

    public void PlayIdle() => animator.Play("Idle");

    public void PlayJump() => animator.Play("Jump");

    public void PlayRun() => animator.Play("Run");

    public void PlaySuperPower(int superPowerIndex) => animator.Play(superPowerIndexName[superPowerIndex]);

    public void PlayWalk() => animator.Play("Walk");
}
