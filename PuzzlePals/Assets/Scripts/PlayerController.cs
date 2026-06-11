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

    private Vector3 jumpDirection;
    private float jumpForce;
    private bool jumpPressed;

    private bool isSprinting;

    //Player Inputs
    private InputManager inputManager;

    public IInteractable currentInteractable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        jumpForce = Mathf.Sqrt(2.0f * Mathf.Abs(Physics.gravity.y) * currentMonster.Stats.jumpHeight);
        jumpDirection = Vector3.up * jumpForce;

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
        Vector2 input = inputManager.MoveAction.ReadValue<Vector2>();

        //Yaw only
        Vector3 camForward = lookAtTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = lookAtTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 movementDirection = (camForward * input.y + camRight * input.x).normalized;

        float speed = currentMonster.Stats.movementSpeed;

        if (isSprinting) speed *= currentMonster.Stats.sprintMultiplier;

        Vector3 moveForce = movementDirection * speed;

        if (moveForce.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(camForward);
        }

        rb.AddForce(moveForce, ForceMode.Force);
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
