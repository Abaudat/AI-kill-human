using UnityEngine;
using UnityEngine.EventSystems;

public class SentencePanelDragTarget : MonoBehaviour, IDropHandler, IPointerEnterHandler
{
    public Animator animator;

    public void OnDrop(PointerEventData eventData)
    {
        WordButton wordButton = eventData.pointerDrag.GetComponent<WordButton>();
        if (wordButton)
        {
            wordButton.PlayWord();
            animator.SetBool("Hovered", false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        WordButton wordButton = eventData?.pointerDrag?.GetComponent<WordButton>();
        if (wordButton)
        {
            animator.SetBool("Hovered", true);
        }
    }
}
