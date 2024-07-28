using UnityEngine;
using UnityEngine.EventSystems;

public class SentencePanelDragTarget : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
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

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        WordButton wordButton = eventData?.pointerDrag?.GetComponent<WordButton>();
        if (wordButton)
        {
            Debug.Log("Exit with drag");
            animator.SetBool("Hovered", false);
        }
    }
}
