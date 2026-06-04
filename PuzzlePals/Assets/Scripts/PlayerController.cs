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

    private Vector3 jumpDirection;
    private float jumpForce;
    private bool jumpPressed;

    //Player Inputs
    private InputManager inputManager;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction interactAction;
    private InputAction attackAction;

    //private InputAction moveAction;
    //private InputAction jumpAction;
    //private InputAction interactAction;

    public IInteractable currentInteractable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        inputManager = InputManager.Instance;
    }

    void Start()
    {
        jumpForce = Mathf.Sqrt(2.0f * Mathf.Abs(Physics.gravity.y) * currentMonster.Stats.jumpHeight);
        jumpDirection = Vector3.up * jumpForce; 
    }

    private void OnEnable()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        interactAction = InputSystem.actions.FindAction("Interact");
        attackAction = InputSystem.actions.FindAction("Attack");

        jumpAction.performed += OnJumpPerformed;
        interactAction.performed += OnInteract;
        attackAction.performed += OnAttacked;
        inputManager.JumpAction.performed += OnJumpPerformed;
        inputManager.InteractAction.performed += OnInteract;
    }

    private void OnDisable()
    {
        jumpAction.performed -= OnJumpPerformed;
        interactAction.performed -= OnInteract;
        attackAction.performed -= OnAttacked;
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        jumpPressed = true;
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        currentInteractable?.Interact();
        Debug.Log(currentInteractable);

    }
    private void OnAttacked(InputAction.CallbackContext ctx)
    {
        currentMonster.UseSuperPower();
    }
    private void FixedUpdate()
    {
        jumpForce = Mathf.Sqrt(2.0f * Mathf.Abs(Physics.gravity.y) * currentMonster.Stats.jumpHeight);
        jumpDirection = Vector3.up * jumpForce;


        Move();
        Jump();


        if (!IsGrounded() && rb.linearVelocity.y < 0)
        {
            rb.AddForce(Vector3.down * Physics.gravity.magnitude * (currentMonster.Stats.fallMultiplier - 1f), ForceMode.Acceleration);
        }

    }

    private void Jump()
    {
        if (jumpPressed && IsGrounded())
        {
            //resets the velocity so if jumping on slopes it should be with the same force
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

            rb.AddForce(jumpDirection, ForceMode.Impulse);
        }

        jumpPressed = false;
    }

    private void Move()
    {
        movementDirection = new Vector3(inputManager.MoveAction.ReadValue<Vector2>().x, 0, inputManager.MoveAction.ReadValue<Vector2>().y).normalized;
        rb.AddForce(movementDirection * currentMonster.Stats.movementSpeed, ForceMode.Force);
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
