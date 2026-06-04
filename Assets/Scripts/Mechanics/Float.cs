using UnityEngine;

public class Float : MonoBehaviour
{
    [Header("Float Settings")]
    public float amplitude = 0.25f;   // altura máxima
    public float frequency = 1f;      // velocidad

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offsetY = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(
            startPos.x,
            startPos.y + offsetY,
            startPos.z
        );
        
        transform.Rotate(0f, 0f, 40f * Time.deltaTime);
        
        float scale = 1f + Mathf.Sin(Time.time * frequency) * 0.05f;
        transform.localScale = new Vector3(scale, scale, 1f);

    }
}
