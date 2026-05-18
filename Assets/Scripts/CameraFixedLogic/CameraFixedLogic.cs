using UnityEngine;

public class CameraFixedLogic : MonoBehaviour
{
    private Camera cam;

    // Estos son los valores de tu diseño (16:9 Landscape)
    private float targetAspect = 16f / 9f; 
    private float baseSize = 5.4f; // La mitad de 10.8 unidades

    void Awake()
    {
        cam = GetComponent<Camera>();
        AdjustCamera();
    }

    // Usamos Update solo para probar en el editor, luego puedes dejarlo solo en Awake
    void Update()
    {
        AdjustCamera();
    }

    void AdjustCamera()
    {
        if (cam == null) return;

        float currentAspect = (float)Screen.width / Screen.height;

        // Si la pantalla es más "larga" que 16:9 (como casi todos los móviles hoy)
        if (currentAspect >= targetAspect)
        {
            cam.orthographicSize = baseSize;
        }
        // Si la pantalla es más "cuadrada" (como un iPad o móvil viejo)
        else
        {
            float differenceInSize = targetAspect / currentAspect;
            cam.orthographicSize = baseSize * differenceInSize;
        }
    }
}