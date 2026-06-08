using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    [SerializeField] private Player player;

    [Header("Sprites")]
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite pressedSprite;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (player != null)
            player.Jump();

        if (buttonImage != null && pressedSprite != null)
            buttonImage.sprite = pressedSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buttonImage != null && normalSprite != null)
            buttonImage.sprite = normalSprite;
    }
}