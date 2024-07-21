using UnityEngine;
using UnityEngine.UI;

public class ExtrasManager : MonoBehaviour
{
    public GameObject extraHumansPanel, extraAIsPanels;
    public Image extraHumansPanelImage, extraAisPanelImage;


    public void ShowExtraHumans()
    {
        extraHumansPanel.transform.localPosition = Vector2.zero;
        extraHumansPanelImage.raycastTarget = true;
    }

    public void HideExtraHumans()
    {
        extraHumansPanel.transform.localPosition = new Vector3(5000, 5000);
        extraHumansPanelImage.raycastTarget = false;
    }

    public void ShowExtraAIs()
    {
        extraAIsPanels.transform.localPosition = Vector2.zero;
        extraAisPanelImage.raycastTarget = true;
    }

    public void HideExtraAIs()
    {
        extraAIsPanels.transform.localPosition = new Vector3(5000, 5000);
        extraAisPanelImage.raycastTarget = false;
    }
}
