using FMODUnity;
using UnityEngine;
/// <summary>
/// This class holds all FMOD eventreferences to easily play sounds from anywhere using the AudioManager
/// Is a singleton so getting a reference is easy from anywhere in the project
/// </summary>
public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance;

    [field: Header("SFX")]
    [field: SerializeField] public EventReference transformation { get; private set; }
    [field: SerializeField] public EventReference buttonClick { get; private set; }
    [field: SerializeField] public EventReference doorOpen { get; private set; }
    [field: SerializeField] public EventReference returnSpawnpoint { get; private set; }
    [field: SerializeField] public EventReference monsterEat { get; private set; }

    [field: SerializeField] public EventReference homeFootsteps { get; private set; }
    [field: SerializeField] public EventReference bed { get; private set; }
    [field: SerializeField] public EventReference dreamState { get; private set; }
    
    [field: SerializeField] public EventReference midaFootsteps { get; private set; }
    [field: SerializeField] public EventReference throwBall { get; private set; }
    [field: SerializeField] public EventReference ghostMode { get; private set; }
    [field: SerializeField] public EventReference julliaFootsteps { get; private set; }
    [field: SerializeField] public EventReference sprint { get; private set; }
    [field: SerializeField] public EventReference happiness { get; private set; }
    [field: SerializeField] public EventReference copkacFootsteps { get; private set; }
    [field: SerializeField] public EventReference copkacJump { get; private set; }
    [field: SerializeField] public EventReference createCupcake { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference clickUI { get; private set; }
    [field: SerializeField] public EventReference star { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference mainMenuMusic { get; private set; }
    [field: SerializeField] public EventReference lobbyMusic { get; private set; }
    [field: SerializeField] public EventReference puzzleMusic { get; private set; }
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return;} // Singleton
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
