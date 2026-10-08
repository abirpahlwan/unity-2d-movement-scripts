using UnityEngine;

/// <summary>
/// PlayerController handles 2D physics-based character movement using Unity's Rigidbody2D.
/// Extracted from ShootingDux Unity 2D Movement Tutorial.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Base movement speed of the player in units per second.")]
    [SerializeField] private float moveSpeed = 5f;

    [Tooltip("Speed multiplier applied when holding down the sprint key (Left Shift).")]
    [SerializeField] private float sprintMultiplier = 1.5f;

    [Tooltip("Impulse force applied during a dash move.")]
    [SerializeField] private float dashForce = 12f;

    [Tooltip("Cooldown time between dashes in seconds.")]
    [SerializeField] private float dashCooldown = 1f;

    [Header("Components & Visuals")]
    [Tooltip("Reference to the SpriteRenderer to flip player graphics based on movement direction.")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Tooltip("Reference to the Animator for playing movement animations.")]
    [SerializeField] private Animator animator;

    // Private Physics & Input Variables
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private float currentSpeed;
    private bool isSprinting;
    private bool canDash = true;
    private bool isDashing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    private void Start()
    {
        rb.freezeRotation = true;
    }

    private void Update()
    {
        ProcessInput();
        FlipSprite();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void ProcessInput()
    {
        if (isDashing) return;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        movementInput = new Vector2(inputX, inputY).normalized;
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = moveSpeed * (isSprinting ? sprintMultiplier : 1f);

        if (Input.GetKeyDown(KeyCode.Space) && canDash && movementInput != Vector2.zero)
        {
            StartCoroutine(PerformDash());
        }
    }

    private void MovePlayer()
    {
        if (isDashing) return;
        rb.velocity = movementInput * currentSpeed;
    }

    private void FlipSprite()
    {
        if (spriteRenderer == null) return;
        if (movementInput.x > 0.1f) spriteRenderer.flipX = false;
        else if (movementInput.x < -0.1f) spriteRenderer.flipX = true;
    }

    private void UpdateAnimations()
    {
        if (animator == null) return;
        animator.SetFloat("Speed", movementInput.sqrMagnitude);
        animator.SetBool("IsSprinting", isSprinting);
        animator.SetBool("IsDashing", isDashing);
    }

    private System.Collections.IEnumerator PerformDash()
    {
        canDash = false;
        isDashing = true;

        rb.velocity = movementInput * dashForce;
        yield return new WaitForSeconds(0.2f);

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }
}