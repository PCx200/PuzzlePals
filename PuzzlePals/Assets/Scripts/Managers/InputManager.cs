using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] public InputActionAsset inputActionAsset;

    #region Input Actions
    private InputAction moveAction;
    public InputAction MoveAction => moveAction;

    private InputAction jumpAction;
    public InputAction JumpAction => jumpAction;

    private InputAction superPower1;
    public InputAction SuperPower1Action => superPower1;

    private InputAction superPower2;
    public InputAction SuperPower2Action => superPower2;

    //public InputAction SeeInvisible;

    private InputAction pauseMenuAction;
    public InputAction PauseMenuAction => pauseMenuAction;
    #endregion

    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        moveAction = inputActionAsset.FindAction("Move");
        jumpAction = inputActionAsset.FindAction("Jump");
        superPower1 = inputActionAsset.FindAction("SuperPower1");
        superPower2 = inputActionAsset.FindAction("SuperPower2");

        pauseMenuAction = inputActionAsset.FindAction("Pause");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        inputActionAsset.Enable();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        inputActionAsset.Enable();
    }

    private void OnDisable()
    {
        inputActionAsset.Disable();
    }
}

