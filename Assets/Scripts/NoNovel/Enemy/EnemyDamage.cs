using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogWarning("El Player no tiene PlayerHealth.cs");
            return;
        }

        playerHealth.TakeDamage(damage, transform.position);
        Debug.Log("ENEMY golpeó al player.");
    }
}
