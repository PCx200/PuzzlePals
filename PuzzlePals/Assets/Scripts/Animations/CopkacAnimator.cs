using System.Collections.Generic;
using UnityEngine;

public class CopkacAnimator : MonoBehaviour, IMonsterAnimator
{
    [SerializeField] private Animator animator;

    [SerializeField] private List<string> superPowerIndexName;

    public void PlayIdle() => animator.Play("Idle");

    public void PlayJump() => animator.Play("Jump");

    public void PlayRun() { }

    public void PlaySuperPower(int superPowerIndex) => animator.Play(superPowerIndexName[superPowerIndex]);

    public void PlayWalk() => animator.Play("Walk");
}
