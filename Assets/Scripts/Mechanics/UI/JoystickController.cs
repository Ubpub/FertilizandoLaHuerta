using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;

    public float Horizontal { get; private set; }

    private float radius;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        background.gameObject.SetActive(false);
        radius = background.sizeDelta.x * 0.5f;

        canvasGroup = background.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = background.gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.position.x > Screen.width / 2f)
            return;

        background.position = eventData.position;
        background.gameObject.SetActive(true);
        handle.anchoredPosition = Vector2.zero;
        canvasGroup.alpha = 1f;

        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvasGroup.alpha == 0f)
            return;

        Vector2 position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,
            eventData.position,
            eventData.pressEventCamera,
            out position
        );

        position = Vector2.ClampMagnitude(position, radius);
        handle.anchoredPosition = position;

        Horizontal = position.x / radius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        handle.anchoredPosition = Vector2.zero;
        Horizontal = 0f;
        canvasGroup.alpha = 0f;
    }
}