using UnityEngine;
/// <summary>
/// This interface is used for the animations belonging to monsters. 
/// In the PlayerController you call the Idle, Walk and Jump animations, because they corespond to all of the monsters.
/// The superpower Animation uses string with the name of the superpower and is assigned inside each superpower when it is performed.
/// </summary>
public interface IMonsterAnimator
{
    void PlayIdle();
    void PlayWalk();
    void PlayRun();
    void PlayJump();

    void PlaySuperPower(string superPowerName);
}
