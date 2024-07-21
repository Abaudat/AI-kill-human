using UnityEngine;
using UnityEngine.EventSystems;

public class ExtraHumansBackground : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public ExtraHumans extraHumans;

    public void OnPointerClick(PointerEventData eventData)
    {
        extraHumans.Hide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        WordButton wordButton = eventData?.pointerDrag?.GetComponent<WordButton>();
        if (wordButton)
        {
            extraHumans.Hide();
        }
    }
}
