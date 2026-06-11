using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> listaCorazones;
    [SerializeField] private Sprite corazonActivado;
    [SerializeField] private Sprite corazonDesactivado;

    public void RestaCorazones(int indice)
    {
        Image imagenCorazon = listaCorazones[indice].GetComponent<Image>();
        imagenCorazon.sprite = corazonDesactivado;
    }

    public void ResetVida()
    {
        foreach (GameObject corazon in listaCorazones)
        {
            Image imagenCorazon = corazon.GetComponent<Image>();
            imagenCorazon.sprite = corazonActivado;
        }
    }
}
