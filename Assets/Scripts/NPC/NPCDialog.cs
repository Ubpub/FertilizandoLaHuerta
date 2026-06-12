using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string characterName;
    public Sprite characterSprite;

    [TextArea(2, 5)]
    public string[] dialogueLines;
}