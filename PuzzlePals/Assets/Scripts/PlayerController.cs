using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public MonsterCharacter currentMonster;

    private Vector3 movementDirection;

    [Header("Jumping Properties")]
    [SerializeField] private Transform legsTransform;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform lookAtTransform;
    
    private Vector3 jumpForce;
    private bool jumpPressed;

    private bool isSprinting;
    private bool isGrounded;

    //Player Inputs
    private InputManager inputManager;

    public IInteractable currentInteractable;
    [SerializeField] private float interactCooldown = 0.2f;
    private float lastInteractTime = -999f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        inputManager = InputManager.Instance;

        inputManager.JumpAction.performed += OnJumpPerformed;
        inputManager.InteractAction.performed += OnInteract;
        inputManager.AttackAction.performed += OnSuperPowerUsed;

        inputManager.SprintAction.performed += OnSprintPerformed;
        inputManager.ReleaseHappiness.performed += OnSuperPowerUsed2;
        inputManager.SprintAction.canceled += OnSprintCanceled;
    }

    private void OnDisable()
    {
        inputManager.JumpAction.performed -= OnJumpPerformed;
        inputManager.InteractAction.performed -= OnInteract;
        inputManager.AttackAction.performed -= OnSuperPowerUsed;

        inputManager.SprintAction.performed -= OnSprintPerformed;
        inputManager.ReleaseHappiness.performed -= OnSuperPowerUsed2;
        inputManager.SprintAction.canceled -= OnSprintCanceled;
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        jumpPressed = true;
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        if (Time.time < lastInteractTime + interactCooldown) return;

        lastInteractTime = Time.time;

        currentInteractable?.Interact();
        Debug.Log(currentInteractable);

    }
    private void OnSuperPowerUsed(InputAction.CallbackContext ctx)
    {
        //if (currentMonster.Name != MonsterCharacter.MonsterName.Mida) return;
        currentMonster.UseSuperPower(0);
    }
    private void OnSuperPowerUsed2(InputAction.CallbackContext ctx)
    {
        currentMonster.UseSuperPower(1);
    }

    private void OnSprintPerformed(InputAction.CallbackContext ctx)
    {
        if (currentMonster.Name != MonsterCharacter.MonsterName.Jullia) return;
        isSprinting = true;
    }

    private void OnSprintCanceled(InputAction.CallbackContext ctx)
    {
        if (currentMonster.Name != MonsterCharacter.MonsterName.Jullia) return;
        isSprinting = false;
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
        
        Move();
        Jump();
    }

    private void Jump()
    {
        jumpForce = Mathf.Sqrt(2.0f * Mathf.Abs(Physics.gravity.y) * currentMonster.Stats.jumpHeight) * Vector3.up;
        if (jumpPressed && IsGrounded())
        {
            //resets the velocity so if jumping on slopes it should be with the same force
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(jumpForce, ForceMode.Impulse);
        }
        jumpPressed = false;
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
}
