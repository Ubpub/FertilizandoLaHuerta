using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxSpeed = 0.05f;
    public float imageWidth = 20f;

    private Transform[] backgrounds;
    private Vector3 lastCameraPos;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        backgrounds = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            backgrounds[i] = transform.GetChild(i);

        lastCameraPos = cameraTransform.position;
    }

    void LateUpdate()
    {
        float camDelta = cameraTransform.position.x - lastCameraPos.x;

        transform.position += Vector3.right * camDelta * parallaxSpeed;

        lastCameraPos = cameraTransform.position;

        foreach (Transform bg in backgrounds)
        {
            float camToBg = cameraTransform.position.x - bg.position.x;

            if (camToBg > imageWidth * 1.5f)
            {
                bg.position += Vector3.right * imageWidth * backgrounds.Length;
            }
            else if (camToBg < -imageWidth * 1.5f)
            {
                bg.position -= Vector3.right * imageWidth * backgrounds.Length;
            }
        }

        float pixelsPerUnit = 32f;

        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x * pixelsPerUnit) / pixelsPerUnit;
        transform.position = pos;
    }
}
