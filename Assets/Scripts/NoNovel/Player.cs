using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    // private PlayerHealth playerHealth;

    [Header("Movement")]
    [SerializeField] private float speed;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    [Header("AttackPoint offset (flipX)")]
    public Vector2 rightOffset = new Vector2(0.45f, 0.0f);
    private float moveDirection;
    bool isGrounded;

    private int movementTouchId = -1;
    private Vector2 movementStartPos;
    [SerializeField] private float swipeDeadZone = 30f;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (playerAnimator == null) playerAnimator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // if (playerHealth != null && playerHealth.controlsLocked) return;
        // if (spriteRenderer == null || attackPoint == null) return;

        Vector2 off = rightOffset;
        if (spriteRenderer.flipX) off.x = -rightOffset.x;

        // attackPoint.localPosition = off;

        ReadInput();
        UpdateGrounded();
        HandleFlip();
        HandleJump();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        // if (playerHealth != null && playerHealth.controlsLocked) return;

        ApplyMovement();
    }

    // ---------- Input ----------
    private void ReadInput()
    {
        // PC controls
        moveDirection = Input.GetAxis("Horizontal");

        // Android controls
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                // Start movement touch on left side
                if (touch.phase == TouchPhase.Began &&
                    touch.position.x < Screen.width / 2f &&
                    movementTouchId == -1)
                {
                    movementTouchId = touch.fingerId;
                    movementStartPos = touch.position;
                }

                // Continue movement while holding that finger
                if (touch.fingerId == movementTouchId)
                {
                    float differenceX = touch.position.x - movementStartPos.x;

                    if (differenceX > swipeDeadZone)
                        moveDirection = 1f;
                    else if (differenceX < -swipeDeadZone)
                        moveDirection = -1f;
                    else
                        moveDirection = 0f;

                    // Stop movement when finger is lifted
                    if (touch.phase == TouchPhase.Ended ||
                        touch.phase == TouchPhase.Canceled)
                    {
                        movementTouchId = -1;
                        moveDirection = 0f;
                    }
                }
            }
        }
    }

    private void HandleJump()
    {
        // PC jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();

        // Android jump: tap right side
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began &&
                touch.position.x > Screen.width / 2f &&
                isGrounded)
            {
                Jump();
            }
        }
    }

    // ---------- Movement ----------
    private void ApplyMovement()
    {
        rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);
        
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void HandleFlip()
    {
        if (moveDirection > 0.01f) spriteRenderer.flipX = false;
        else if (moveDirection < -0.01f) spriteRenderer.flipX = true;
    }

    // ---------- Ground ----------
    private void UpdateGrounded()
    {
        if (groundCheck == null) return;

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    // ---------- Animator ----------
    private void UpdateAnimator()
    {
        playerAnimator.SetBool("Running", Mathf.Abs(moveDirection) > 0.01f);
        // playerAnimator.SetBool("Jumping", !isGrounded);
    }

    void OnDrawGizmosSelected()
    {
        if(groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
