using UnityEngine;

public class HighJump : SuperPower
{
    private PlayerController player;
    private Rigidbody rb;
    [SerializeField] private float jumpHeight;
    [SerializeField] private ParticleSystem highJump;
    [Header("Jumping Properties")]
    [SerializeField] private Transform legsTransform;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        rb = GetComponentInParent<Rigidbody>();
    }
    public override void SuperPowerPressed()
    {
        Debug.Log("Jump() called");

        bool isGrounded = IsGrounded();

        Debug.Log($"Grounded: {isGrounded}");

        Vector3 jumpForce = Mathf.Sqrt(2.0f * Mathf.Abs(Physics.gravity.y) * jumpHeight) * Vector3.up;
        if (isGrounded)
        {
            //resets the velocity so if jumping on slopes it should be with the same force
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(jumpForce, ForceMode.Impulse);
            highJump.Play();
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(legsTransform.position, groundCheckRadius, groundLayer);
    }
}
