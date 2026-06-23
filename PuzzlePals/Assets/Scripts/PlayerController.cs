using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;
using UnityEngine.Windows;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float normalJumpHeight;

    // maybe put monster transformation data in a separate script (better architecture) 
    public MonsterCharacter currentMonster;

    public List<MonsterCharacter> monsters = new List<MonsterCharacter>();

    private Vector3 movementDirection;

    [Header("Jumping Properties")]
    [SerializeField] private Transform legsTransform;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform lookAtTransform;
    
    private Vector3 jumpForce;
    private bool jumpPressed;

    [HideInInspector] public bool isSprinting;
    [HideInInspector] private bool isGrounded;

    // Player Inputs
    private InputManager inputManager;

    // should probably not be in player controller
    public IInteractable currentInteractable;
    [SerializeField] private float interactCooldown = 0.2f;
    private float lastInteractTime = -999f;

    // Audio
    private EventInstance homeFootsteps;
    private EventInstance midaFootsteps;
    private EventInstance julliaFootsteps;
    private EventInstance copkacFootsteps;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        inputManager = InputManager.Instance;

        inputManager.JumpAction.performed += OnJumpPerformed;
        inputManager.SuperPower1Action.performed += OnSuperPower1Pressed;
        inputManager.SuperPower2Action.performed += OnSuperPower2Pressed;
        inputManager.SuperPower1Action.canceled += OnSuperPower1Released;
        inputManager.SuperPower2Action.canceled += OnSuperPower2Released;

        FindCurrentMonster();
        Debug.Log(inputManager.SuperPower2Action.enabled);

        //homeFootsteps = AudioManager.Instance.CreateInstance(FMODEvents.Instance.homeFootsteps);
        //midaFootsteps = AudioManager.Instance.CreateInstance(FMODEvents.Instance.midaFootsteps);
        //julliaFootsteps = AudioManager.Instance.CreateInstance(FMODEvents.Instance.julliaFootsteps);
        //copkacFootsteps = AudioManager.Instance.CreateInstance(FMODEvents.Instance.copkacFootsteps);
    }

    private void OnDisable()
    {
        inputManager.JumpAction.performed -= OnJumpPerformed;
        inputManager.SuperPower1Action.performed -= OnSuperPower1Pressed;
        inputManager.SuperPower2Action.performed -= OnSuperPower2Pressed;
        inputManager.SuperPower1Action.canceled -= OnSuperPower1Released;
        inputManager.SuperPower2Action.canceled -= OnSuperPower2Released;
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {     
        Jump();
        Debug.Log("Jump pressed");
    }

    #region SuperPowers

    private void OnSuperPower1Pressed(InputAction.CallbackContext ctx)
    {
        currentMonster.SuperPowerPressed(0);
        Debug.Log("SuperPower 1 Pressed");
    }
    private void OnSuperPower2Pressed(InputAction.CallbackContext ctx)
    {
        currentMonster.SuperPowerPressed(1);
        Debug.Log("SuperPower 2 Pressed");
    }
    private void OnSuperPower1Released(InputAction.CallbackContext ctx)
    {
        currentMonster.SuperPowerReleased(0);
    }
    private void OnSuperPower2Released(InputAction.CallbackContext ctx)
    {
        currentMonster.SuperPowerReleased(1);
    }

    #endregion
    
    #region Movement

    private void Jump()
    {
        Debug.Log("Jump() called");
        
        isGrounded = IsGrounded();

        Debug.Log($"Grounded: {isGrounded}");

        jumpForce = Mathf.Sqrt(2.0f * Mathf.Abs(Physics.gravity.y) * normalJumpHeight) * Vector3.up;
        if (isGrounded)
        {
            //resets the velocity so if jumping on slopes it should be with the same force
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(jumpForce, ForceMode.Impulse);
        }
        
    }

    private void Move()
    {
        Vector2 input = inputManager.MoveAction.ReadValue<Vector2>();
        //Yaw only
        Vector3 camForward = lookAtTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = lookAtTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 movementDirection = (camForward * input.y + camRight * input.x).normalized;

        float acceleration = currentMonster.Stats.acceleration;

        if (isSprinting) acceleration *= currentMonster.Stats.sprintMultiplier;
        if (!isGrounded) acceleration *= currentMonster.Stats.airMultiplier;
        
        Vector3 moveForce = movementDirection * acceleration;
        Vector3 frictionForce = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z) * currentMonster.Stats.friction;
        if (!isGrounded) frictionForce = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z) * currentMonster.Stats.airFriction;
        
        rb.AddForce(moveForce - frictionForce, ForceMode.Force);
        
        if (moveForce.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        }
    }

    #endregion
    
    private void FixedUpdate()
    {
        Move();
        UpdateSound();
    }
    private void FindCurrentMonster()
    {
        foreach (MonsterCharacter monster in monsters)
        {
            if (monster.isActiveAndEnabled)
            {
                currentMonster = monster;
            }
        }
        Debug.Log("Current monster is " +  currentMonster.Name);
    }
    private bool IsGrounded()
    {
        return Physics.CheckSphere(legsTransform.position, groundCheckRadius, groundLayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        { 
        
            currentInteractable = interactable;
            Debug.Log("Trigger with: " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable) &&
            currentInteractable == interactable)
        {
            Debug.Log("Exited: " + other.name);
            currentInteractable = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(legsTransform.position, groundCheckRadius);
    }

    // Footsteps sounds (currently every monster has Home's footsteps)
    private void UpdateSound()
    {
        if (inputManager.MoveAction.IsPressed())
        {
            PLAYBACK_STATE playbackState;
            currentMonster.footsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED) || playbackState.Equals(PLAYBACK_STATE.STOPPING))
            {
                currentMonster.footsteps.start();
                Debug.Log("Footsteps are being played");
            }
        }
        else
        {
            currentMonster.footsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }    
    }
}
