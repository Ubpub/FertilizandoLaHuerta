using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;

    [Header("Death")]
    public float destroyDelay = 0.3f;

    private Animator anim;
    private Collider2D[] colliders;
    private Rigidbody2D rb;
    private bool isDead;

    void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        colliders = GetComponentsInChildren<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int dmg)
    {
        if (isDead) return;

        currentHealth -= dmg;
        Debug.Log($"ENEMY recibió {dmg}. Vida: {currentHealth}");

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("ENEMY muerto.");

        if (anim != null) anim.SetTrigger("Die");

        // Desactiva colliders para que no siga golpeando/colisionando
        foreach (var c in colliders) c.enabled = false;

        // Opcional: para que no se mueva más
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false; // o rb.bodyType = RigidbodyType2D.Kinematic;
        }

        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
