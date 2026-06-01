using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerHealth player;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("Player"))
        {
        //    player.TakeDamage();
           Debug.Log("El enemigo ha hecho daño al jugador");
        }
    }
}

