using UnityEngine;
using UnityEngine.EventSystems;

public class ExtrasBackground : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public ExtrasManager extras;

    public void OnPointerClick(PointerEventData eventData)
    {
        extras.HideExtras();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        WordButton wordButton = eventData?.pointerDrag?.GetComponent<WordButton>();
        if (wordButton)
        {
            extras.HideExtras();
        }
    }
}
