using UnityEngine;

public class OtherHumansButton : MonoBehaviour
{
    public void ShowOtherHumansPanel()
    {
        GameObject.FindGameObjectWithTag("ExtraHumansPanel").SetActive(true);
        FindObjectOfType<GlobalSound>().PlayUiClick();
    }
}
