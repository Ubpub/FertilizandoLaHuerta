using UnityEngine;

public class FallZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        PlayerHealth ph = col.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            // Respawn sin “morir” (solo reset)
            ph.Respawn();
        }
        else
        {
            // Fallback si no hay PlayerHealth
            col.transform.position = Vector3.zero;
        }
    }
}