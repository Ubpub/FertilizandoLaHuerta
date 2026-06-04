using System;
using UnityEngine;

public class Fertilizante : MonoBehaviour
{
    [Header("Puntos")]
    public static Action<int> fertilizanteRecolectado;
    [SerializeField] private int valorPuntos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Recolectar();
        }
    }

    public void Recolectar()
    {
        fertilizanteRecolectado?.Invoke(valorPuntos);
        Destroy(gameObject);
    }
}
