using UnityEngine;
using UnityEngine.UI;

public class ExtraHumans : MonoBehaviour
{
    public GameObject extraHumansPanel;
    public Image extraHumansPanelImage;


    public void Show()
    {
        extraHumansPanel.transform.localPosition = Vector2.zero;
        extraHumansPanelImage.raycastTarget = true;
    }

    public void Hide()
    {
        extraHumansPanel.transform.localPosition = new Vector3(5000, 5000);
        extraHumansPanelImage.raycastTarget = false;
    }
}
