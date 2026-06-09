using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    [SerializeField] private PlayerAttack player;

    [Header("Sprites")]
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite pressedSprite;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (player != null)
            player.StartAttack();

        if (buttonImage != null && pressedSprite != null)
            buttonImage.sprite = pressedSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buttonImage != null && normalSprite != null)
            buttonImage.sprite = normalSprite;
    }
}
