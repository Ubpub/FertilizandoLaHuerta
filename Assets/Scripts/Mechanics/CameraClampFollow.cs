using UnityEngine;

public class CameraClampFollow2D : MonoBehaviour
{
    public Transform target;

    [Header("Follow")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothTime = 0.12f;

    [Header("Bounds (World Units)")]
    public float minX, maxX;
    public float minY, maxY;

    private Vector3 velocity;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = target.position + offset;

        // Clamp para no salir del escenario
        desired.x = Mathf.Clamp(desired.x, minX, maxX);
        desired.y = Mathf.Clamp(desired.y, minY, maxY);

        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
    }
}
