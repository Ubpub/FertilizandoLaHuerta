using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 3;
    public int currentHealth;

    [Header("Invincibility")]
    public float invincibilityTime = 0.3f;
    private bool isInvincible;

    [Header("Knockback (optional)")]
    public bool useKnockback = true;
    public float knockbackForce = 7f;
    public float knockbackForceX = 8f;
    public float knockbackForceY = 0f; // puedes poner 1f si quieres un pequeño rebote
    public float knockbackLockTime = 0.25f;
    public bool controlsLocked;

    [Header("Respawn")]
    [SerializeField] private Transform respawnPoint;

    [Header("Celeste Feel")]
    public float hitStopTime = 0.06f;

    [Header("Flicker")]
    public SpriteRenderer sr;
    public float flickerInterval = 0.08f;

    [Header("UI")]
    [SerializeField] UIManager uIManager;

    private Animator playerAnimator;

    private Rigidbody2D rb;
    private Coroutine flickerRoutine;

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (playerAnimator == null) playerAnimator = GetComponent<Animator>();
        if (sr == null) sr = GetComponentInChildren<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, Vector2 hitFromPosition)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        uIManager.RestaCorazones(currentHealth);
        Debug.Log($"PLAYER recibió {damage} de daño. Vida: {currentHealth}/{maxHealth}");
        StartCoroutine(HitStopCoroutine());

        if (useKnockback && rb != null)
        {

            playerAnimator.SetTrigger("Hurting");

            float direction = Mathf.Sign(transform.position.x - hitFromPosition.x);
            if (direction == 0) direction = 1;

            StartCoroutine(LockControlsCoroutine());

            // Empuje inmediato y claro hacia atrás
            rb.linearVelocity = new Vector2(direction * knockbackForceX, rb.linearVelocityY + knockbackForceY);

            Debug.Log($"KNOCKBACK aplicado dir={direction}, velX={rb.linearVelocityX}");
        }

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(InvincibilityCoroutine(invincibilityTime));
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        isInvincible = true;
        // Aquí luego puedes hacer parpadeo del sprite, sonido, etc.
        if (flickerRoutine != null) StopCoroutine(flickerRoutine);
        flickerRoutine = StartCoroutine(FlickerCoroutine(duration));

        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    private IEnumerator LockControlsCoroutine()
    {
        controlsLocked = true;
        yield return new WaitForSeconds(knockbackLockTime);
        controlsLocked = false;
    }

    private void Die()
    {
        Debug.Log("PLAYER ha muerto.");
        uIManager.ResetVida();
        Respawn();
    }

    public void Respawn()
    {
        // restaurar vida
        currentHealth = maxHealth;

        // reset invencibilidad
        isInvincible = false;
        controlsLocked = false;
        
        // reset velocity
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;


        // mover al spawn
        transform.position = respawnPoint.position;

        Debug.Log("PLAYER respawneó con la vida llena.");
    }

    private IEnumerator HitStopCoroutine()
    {
        float prevScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(hitStopTime);
        Time.timeScale = prevScale;
    }

    private IEnumerator FlickerCoroutine(float duration)
    {
        if (sr == null) yield break;

        float timer = 0f;
        bool visible = true;

        while (timer < duration)
        {
            visible = !visible;
            sr.enabled = visible;

            yield return new WaitForSeconds(flickerInterval);
            timer += flickerInterval;
        }

        sr.enabled = true;
    }

}
