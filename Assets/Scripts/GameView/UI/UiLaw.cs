using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiLaw : MonoBehaviour
{
    public TMP_Text lawText;
    public Animator animator;

    public void Populate(string law)
    {
        lawText.text = law;
    }

    public void Flash()
    {
        animator.SetTrigger("Flash");
    }
}
