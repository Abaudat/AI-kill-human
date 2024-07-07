using UnityEngine;
using UnityEngine.EventSystems;

public class SentencePanelDragTarget : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        WordButton wordButton = eventData.pointerDrag.GetComponent<WordButton>();
        if (wordButton)
        {
            wordButton.PlayWord();
        }
    }
}
