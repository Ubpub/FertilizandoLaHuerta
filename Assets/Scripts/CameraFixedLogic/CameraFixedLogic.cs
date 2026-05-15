using UnityEngine;

public class CameraFixedLogic : MonoBehaviour
{
    [Header("Área de juego mínima visible")]
    public float baseWidth = 19.2f;
    public float baseHeight = 10.8f;

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        Screen.fullScreen = true;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        float targetAspect = baseWidth / baseHeight;
        float currentAspect = (float)Screen.width / Screen.height;

        // Si la pantalla es más alargada que nuestro diseño (como el Xiaomi)
        if (currentAspect > targetAspect)
        {
            cam.orthographicSize = baseHeight / 2f;
        }
        // Si la pantalla es más cuadrada (como un iPad o móvil antiguo)
        else
        {
            float differenceInSize = targetAspect / currentAspect;
            cam.orthographicSize = (baseHeight / 2f) * differenceInSize;
        }
    }
}