using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Image characterImage;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private NPCDialogue currentNPC;
    private int lineIndex;

    public void StartDialogue(NPCDialogue npc)
    {
        currentNPC = npc;
        lineIndex = 0;

        dialoguePanel.SetActive(true);
        characterImage.sprite = npc.characterSprite;
        nameText.text = npc.characterName;

        ShowLine();
    }

    public void ShowNextLine()
    {
        lineIndex++;

        if (lineIndex >= currentNPC.dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    private void ShowLine()
    {
        dialogueText.text = currentNPC.dialogueLines[lineIndex];
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        currentNPC = null;
    }
}