using System.Collections;
using UnityEngine;

public class EnemyPatrolChase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [Header("Patrol")]
    [SerializeField] private float patrolSpeed = 1.5f;
    [SerializeField] private float patrolDistance = 3f;

    [Header("Chase")]
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float chaseSpeed = 3f;
    [SerializeField] private float stopDistance = 1f;

    [Header("Pause After Hit")]
    [SerializeField] private float pauseAfterHit = 0.4f;
    private bool isPaused;

    private float leftLimit;
    private float rightLimit;
    private bool movingRight = true;
    private bool isChasing;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
                player = playerObject.transform;
        }
    }

    private void Start()
    {
        leftLimit = transform.position.x - patrolDistance;
        rightLimit = transform.position.x + patrolDistance;
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        isChasing = distanceToPlayer <= chaseRange && distanceToPlayer > stopDistance;

        if (animator != null)
            animator.SetBool("Running", Mathf.Abs(rb.linearVelocity.x) > 0.01f);
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        if (isPaused)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        if (isChasing)
            ChasePlayer();
        else
            Patrol();
    }

    private void Patrol()
    {
        if (movingRight)
        {
            rb.linearVelocity = new Vector2(patrolSpeed, rb.linearVelocity.y);

            if (spriteRenderer != null)
                spriteRenderer.flipX = true;

            if (transform.position.x >= rightLimit)
                movingRight = false;
        }
        else
        {
            rb.linearVelocity = new Vector2(-patrolSpeed, rb.linearVelocity.y);

            if (spriteRenderer != null)
                spriteRenderer.flipX = false;

            if (transform.position.x <= leftLimit)
                movingRight = true;
        }
    }

    private void ChasePlayer()
    {
        float directionX = Mathf.Sign(player.position.x - transform.position.x);

        rb.linearVelocity = new Vector2(directionX * chaseSpeed, rb.linearVelocity.y);

        if (spriteRenderer != null)
            spriteRenderer.flipX = directionX > 0;
    }

    public void PauseAfterHittingPlayer()
    {
        StartCoroutine(PauseRoutine());
    }

    private IEnumerator PauseRoutine()
    {
        isPaused = true;
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        yield return new WaitForSeconds(pauseAfterHit);

        isPaused = false;
    }
}
