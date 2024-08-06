using UnityEngine;
using UnityEngine.UI;

public class ExtrasManager : MonoBehaviour
{
    public GameObject extrasPanel;
    public Image extrasPanelImage;


    public void ShowExtras()
    {
        extrasPanel.transform.localPosition = Vector2.zero;
        extrasPanelImage.raycastTarget = true;
    }

    public void HideExtras()
    {
        extrasPanel.transform.localPosition = new Vector3(5000, 5000);
        extrasPanelImage.raycastTarget = false;
    }
}
