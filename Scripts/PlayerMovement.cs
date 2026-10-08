using UnityEngine;

/// <summary>
/// PlayerMovement provides modular 2D physics movement with customizable acceleration,
/// deceleration, and top-down vs side-scroller movement options.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public enum MovementType { TopDown2D, SideScroller2D }

    [Header("Movement Mode")]
    [SerializeField] private MovementType movementType = MovementType.TopDown2D;

    [Header("Physics & Acceleration")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 40f;

    [Header("Jump Settings (SideScroller Only)")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private Vector2 targetInput;
    private Vector2 currentVelocity;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = (movementType == MovementType.TopDown2D) ? Input.GetAxisRaw("Vertical") : 0f;

        targetInput = new Vector2(x, y).normalized;

        if (movementType == MovementType.SideScroller2D && groundCheckPoint != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        if (movementType == MovementType.TopDown2D)
        {
            Vector2 targetVelocity = targetInput * moveSpeed;
            float rate = (targetInput.magnitude > 0) ? acceleration : deceleration;

            currentVelocity = Vector2.MoveTowards(rb.velocity, targetVelocity, rate * Time.fixedDeltaTime);
            rb.velocity = currentVelocity;
        }
        else
        {
            float targetX = targetInput.x * moveSpeed;
            float newX = Mathf.MoveTowards(rb.velocity.x, targetX, acceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(newX, rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}