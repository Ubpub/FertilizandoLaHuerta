using System;
using TMPro;
using UnityEngine;

public class ContadorFertilizante : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoFertilizantes;
    [SerializeField] private int cantidadFertilizante;

    public void Start()
    {
        ActualizarTexto();
    }

    public void OnEnable()
    {
        Fertilizante.fertilizanteRecolectado += SumarFertilizantes;
    }

    public void OnDisable()
    {
        Fertilizante.fertilizanteRecolectado -= SumarFertilizantes;
    }

    private void SumarFertilizantes(int cantidad)
    {
        cantidadFertilizante += cantidad;
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        textoFertilizantes.text = cantidadFertilizante.ToString();
    }
}
