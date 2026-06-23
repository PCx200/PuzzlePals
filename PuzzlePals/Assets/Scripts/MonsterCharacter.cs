using UnityEngine;
using System.Collections.Generic;
using FMOD.Studio;

public class MonsterCharacter : MonoBehaviour
{
    public enum MonsterName { Mida, Copkac, Home, Jullia }

    [SerializeField]  private MonsterName monsterName;
    public MonsterName Name => monsterName;

    [SerializeField] private MonsterStatsSO stats;

    public MonsterStatsSO Stats => stats;

    [SerializeField] private List<SuperPower> superPowers;

    public EventInstance footsteps;

    private void Start()
    {
        SetFootSteps();
    }
    private void SetFootSteps()
    {
        switch (monsterName)
        {
            case MonsterName.Home:
                footsteps = AudioManager.Instance.CreateInstance(FMODEvents.Instance.homeFootsteps);
                break;
            case MonsterName.Mida:
                footsteps = AudioManager.Instance.CreateInstance(FMODEvents.Instance.midaFootsteps);
                break;

            case MonsterName.Copkac:
                footsteps = AudioManager.Instance.CreateInstance(FMODEvents.Instance.copkacFootsteps);
                break;           

            case MonsterName.Jullia:
                footsteps = AudioManager.Instance.CreateInstance(FMODEvents.Instance.julliaFootsteps);
                break;

            default:
                Debug.LogWarning($"No footsteps assigned for {monsterName}");
                break;
        }
    }

    public void SuperPowerPressed(int superPowerNumber)
    {
        if (superPowers == null || superPowers[superPowerNumber] == null) return;
        superPowers[superPowerNumber].SuperPowerPressed();
    }
    public void SuperPowerReleased(int superPowerNumber)
    {
        if (superPowers == null || superPowers[superPowerNumber] == null) return;
        superPowers[superPowerNumber].SuperPowerReleased();
    }
}
