using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class JuliaAnimator : MonoBehaviour, IMonsterAnimator
{
    [SerializeField] private Animator animator;

    public void PlayIdle() => animator.Play("Idle");

    public void PlayJump() => animator.Play("Jump");

    public void PlayRun() => animator.Play("Run");

    public void PlaySuperPower(string superPowerName) => animator.Play(superPowerName);

    public void PlayWalk() => animator.Play("Walk");

    public void SetBool(string name, bool state) => animator.SetBool(name, state);

    public void SetTrigger(string name) => animator.SetTrigger(name);

    public Animator GetAnimator() => animator;
}
