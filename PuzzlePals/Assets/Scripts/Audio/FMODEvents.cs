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
    [field: SerializeField] public EventReference kaboom { get; private set; }
    
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
