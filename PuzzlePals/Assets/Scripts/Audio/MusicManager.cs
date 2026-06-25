using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class MusicManager : MonoBehaviour
{
    public bool menu;
    public bool puzzle;
    
    private EventInstance ambientEventInstance;
    private EventInstance puzzleMusicEventInstance;
    private void Start()
    {
        if (menu)
        {
            ambientEventInstance = AudioManager.Instance.CreateInstance(FMODEvents.Instance.mainMenuMusic);
            ambientEventInstance.start();
        }

        if (puzzle)
        {
            puzzleMusicEventInstance = AudioManager.Instance.CreateInstance(FMODEvents.Instance.puzzleMusic);
            puzzleMusicEventInstance.start();
        }
    }

    /*private void OnDisable()
    {
        ambientEventInstance.stop(STOP_MODE.ALLOWFADEOUT);
        puzzleMusicEventInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }*/
}
