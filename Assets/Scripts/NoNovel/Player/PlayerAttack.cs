using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D attackCollider;

    [Header("Attack")]
    [SerializeField] private float attackDuration = 0.2f;
    [SerializeField] private int damage = 1;

    private bool isAttacking;

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (attackCollider != null)
            attackCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            StartAttack();
    }

    public void StartAttack()
    {
        if (isAttacking) return;

        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        animator.SetBool("Attacking", true);

        if (attackCollider != null)
            attackCollider.enabled = true;

        yield return new WaitForSeconds(attackDuration);

        if (attackCollider != null)
            attackCollider.enabled = false;

        animator.SetBool("Attacking", false);

        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isAttacking) return;

        if (col.CompareTag("Enemy"))
        {
            EnemyHealth eh = col.GetComponent<EnemyHealth>();
            if (eh != null)
            {
                eh.TakeDamage(damage, transform.position);
                Debug.Log("Enemigo golpeado por el jugador");
            }
        }
    }
}
