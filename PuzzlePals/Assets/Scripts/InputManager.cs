using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private InputActionAsset inputActionAsset;

    #region Input Actions
    private InputAction moveAction;
    public InputAction MoveAction => moveAction;

    private InputAction jumpAction;
    public InputAction JumpAction => jumpAction;

    private InputAction interactAction;
    public InputAction InteractAction => interactAction;

    private InputAction attackAction;
    public InputAction AttackAction => attackAction;

    private InputAction sprintAction;
    public InputAction SprintAction => sprintAction;

    private InputAction dreamAction;
    public InputAction DreamAction => dreamAction;

    private InputAction createCupcakeAction;
    public InputAction CreateCupcakeAction => createCupcakeAction;

    private InputAction bounceAction;

    private InputAction axis;

    public InputAction Axis => axis;

    //public InputAction SeeInvisible;
    public InputAction BounceAction => bounceAction;

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
        interactAction = inputActionAsset.FindAction("Interact");
        attackAction = inputActionAsset.FindAction("Attack");
        sprintAction = inputActionAsset.FindAction("Sprint");
        dreamAction = inputActionAsset.FindAction("Dream");
        createCupcakeAction = inputActionAsset.FindAction("CreateCupcake");
        bounceAction = inputActionAsset.FindAction("Bounce");
        axis = inputActionAsset.FindAction("Axis");

        pauseMenuAction = inputActionAsset.FindAction("Pause");

        //SeeInvisible = inputActionAsset.FindAction("SeeInvisible");
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

