using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class MusicManager : MonoBehaviour
{
    public bool menu;
    public bool puzzle;
    
    private EventInstance menuEventInstance;
    private EventInstance puzzleMusicEventInstance;
    private EventInstance ambientMusicEventInstance;
    private void Start()
    {
        if (menu)
        {
            menuEventInstance = AudioManager.Instance.CreateInstance(FMODEvents.Instance.mainMenuMusic);
            menuEventInstance.start();
        }

        if (puzzle)
        {
            puzzleMusicEventInstance = AudioManager.Instance.CreateInstance(FMODEvents.Instance.puzzleMusic);
            puzzleMusicEventInstance.start();
            ambientMusicEventInstance = AudioManager.Instance.CreateInstance(FMODEvents.Instance.ambience);
            ambientMusicEventInstance.start();
        }
    }

    /*private void OnDisable()
    {
        ambientEventInstance.stop(STOP_MODE.ALLOWFADEOUT);
        puzzleMusicEventInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }*/
}
