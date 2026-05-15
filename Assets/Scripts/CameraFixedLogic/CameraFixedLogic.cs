using UnityEngine;

public class CameraFixedLogic : MonoBehaviour
{
    public float fixedWidthUnits = 19.2f; 

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        Screen.fullScreen = true;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        float currentAspect = (float)Screen.width / Screen.height;
        cam.orthographicSize = (fixedWidthUnits / 2f) / currentAspect;
    }
}