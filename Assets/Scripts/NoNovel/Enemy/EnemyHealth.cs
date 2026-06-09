using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 1;
    private int currentHealth;

    [Header("Death")]
    public float destroyDelay = 0.6f;

    [Header("Knockback")]
    [SerializeField] private float knockbackForceX = 4f;
    [SerializeField] private float knockbackForceY = 0f;
    [SerializeField] private float knockbackTime = 0.2f;

    private Animator anim;
    private Collider2D[] colliders;
    private Rigidbody2D rb;
    private EnemyPatrolChase patrolChase;
    private bool isDead;
    private bool isKnockedBack;
    public bool IsKnockedBack => isKnockedBack;

    void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        colliders = GetComponentsInChildren<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        patrolChase = GetComponent<EnemyPatrolChase>();
    }

    public void TakeDamage(int dmg, Vector2 hitFromPosition)
    {
        if (isDead) return;

        currentHealth -= dmg;
        Debug.Log($"ENEMY recibió {dmg}. Vida: {currentHealth}");

        if (rb != null)
        {
            float direction = Mathf.Sign(transform.position.x - hitFromPosition.x);
            if (direction == 0) direction = 1;

            StartCoroutine(KnockbackRoutine(direction));
        }

        if (currentHealth <= 0)
            Die();
        else if (anim != null)
            anim.SetTrigger("Hurt");
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("ENEMY muerto.");

        if (anim != null)
            anim.SetTrigger("Die");
        
        if (patrolChase != null)
            patrolChase.enabled = false;

        // Desactiva colliders para que no siga golpeando/colisionando
        foreach (var c in colliders) c.enabled = false;

        // Opcional: para que no se mueva más
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            // rb.simulated = false; // o rb.bodyType = RigidbodyType2D.Kinematic;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator KnockbackRoutine(float direction)
    {
        isKnockedBack = true;

        rb.linearVelocity = new Vector2(direction * knockbackForceX, knockbackForceY);

        yield return new WaitForSeconds(knockbackTime);
        isKnockedBack = false;
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
