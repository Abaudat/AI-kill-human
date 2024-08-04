using UnityEngine;

public class ProgressPanelManager : MonoBehaviour
{
    public Animator animator;
    public GameObject progressPanelBackground;

    private bool isShown = false;

    public void ToggleVisibility()
    {
        if (isShown)
        {
            animator.SetTrigger("Hide");
            isShown = false;
            progressPanelBackground.SetActive(false);
        }
        else
        {
            animator.SetTrigger("Show");
            isShown = true;
            progressPanelBackground.SetActive(true);
        }
    }
}
