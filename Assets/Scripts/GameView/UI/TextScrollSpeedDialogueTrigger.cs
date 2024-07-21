using UnityEngine;
using UnityEngine.EventSystems;

public class TextScrollSpeedDialogueTrigger : MonoBehaviour, IPointerUpHandler
{
    public Dialogue textScrollSpeedChangedDialogue;

    private DialogueManager dialogueManager;
    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dialogueManager.StartDialogue(textScrollSpeedChangedDialogue);
    }
}
