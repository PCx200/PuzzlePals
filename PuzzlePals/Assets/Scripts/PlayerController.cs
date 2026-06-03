using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float movementSpeed;

    private Vector3 movementDirection;

    [Header("Jumping Properties")]
    [Tooltip("The amount of units the jump is going to be.")]
    [SerializeField] private float jumpHeight;
    [Tooltip("Increases the falling speed.")]
    [SerializeField] private float fallMultiplier;
    [SerializeField] private Transform legsTransform;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    private Vector3 jumpDirection;
    private float jumpForce;
    private bool jumpPressed;

    //Player Inputs
    private InputAction moveAction;
    private InputAction jumpAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        jumpForce = Mathf.Sqrt(2.0f * Mathf.Abs(Physics.gravity.y) * jumpHeight);
        jumpDirection = Vector3.up * jumpForce; 
    }

    private void OnEnable()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        jumpAction.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        jumpAction.performed -= OnJumpPerformed;
    }

    private void FixedUpdate()
    {
        jumpForce = Mathf.Sqrt(2.0f * Mathf.Abs(Physics.gravity.y) * jumpHeight);
        jumpDirection = Vector3.up * jumpForce;


        Move();
        Jump();

        if (!IsGrounded() && rb.linearVelocity.y < 0)
        {
            rb.AddForce(Vector3.down * Physics.gravity.magnitude * (fallMultiplier - 1f), ForceMode.Acceleration);
        }

    }
    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        jumpPressed = true;
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
        movementDirection = new Vector3(moveAction.ReadValue<Vector2>().x, 0, moveAction.ReadValue<Vector2>().y).normalized;
        rb.AddForce(movementDirection * movementSpeed, ForceMode.Force);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(legsTransform.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(legsTransform.position, groundCheckRadius);
    }
}
