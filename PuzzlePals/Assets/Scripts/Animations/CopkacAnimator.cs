using System.Collections.Generic;
using UnityEngine;

public class CopkacAnimator : MonoBehaviour, IMonsterAnimator
{
    [SerializeField] private Animator animator;

    public void PlayIdle() => animator.Play("Idle");

    public void PlayJump() => animator.Play("Jump");

    public void PlayRun() { }

    public void PlaySuperPower(string superPowerName) => animator.Play(superPowerName);

    public void PlayWalk() => animator.Play("Walk");
}
