using UnityEngine;
using System.Collections.Generic;
using FMOD.Studio;
using System.Runtime.CompilerServices;

public class MonsterCharacter : MonoBehaviour
{
    PlayerController player;

    public enum MonsterName { Mida, Copkac, Home, Jullia }

    [SerializeField]  private MonsterName monsterName;
    public MonsterName Name => monsterName;

    [SerializeField] private ParticleSystem footStepsPartEff;

    [SerializeField] private MonsterStatsSO stats;

    public MonsterStatsSO Stats => stats;

    [SerializeField] private List<SuperPower> superPowers;

    public EventInstance footsteps;
    private InputManager inputManager;

    public IMonsterAnimator Animator;

    private void Awake()
    {
        SetAnimator();
    }

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();

        //SetFootSteps();
        inputManager = InputManager.Instance;
    }


    private void Update()
    {
       //UpdateSound();
    }
    private void SetAnimator()
    {
        Animator = GetComponent<IMonsterAnimator>();
        if (Animator == null)
            Debug.LogError($"No Animator found on {name}");
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
    private void UpdateSound()
    {
        //if (inputManager.MoveAction.IsPressed() && player.Grounded)
        //{
        //    PLAYBACK_STATE playbackState;
        //    footsteps.getPlaybackState(out playbackState);
        //    if (playbackState.Equals(PLAYBACK_STATE.STOPPED) || playbackState.Equals(PLAYBACK_STATE.STOPPING))
        //    {
        //        footsteps.start();
        //        if(footStepsPartEff !=null) footStepsPartEff.Play();
        //        Debug.Log("Footsteps are being played");
        //    }
        //}
        //else
        //{
        //    footsteps.stop(STOP_MODE.ALLOWFADEOUT);
        //    if (footStepsPartEff != null) footStepsPartEff.Stop();
        //}
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
