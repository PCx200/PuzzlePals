using UnityEngine;

public interface IMonsterAnimator
{
    void PlayIdle();
    void PlayWalk();
    void PlayRun();
    void PlayJump();

    void PlaySuperPower(int superPowerIndex);
}
