using UnityEngine;

public class GlobalSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sentenceImpossibleClip, uiClick, uiCancel;

    public void PlayImpossible()
    {
        audioSource.PlayOneShot(sentenceImpossibleClip);
    }

    public void PlayUiClick()
    {
        audioSource.PlayOneShot(uiClick);
    }

    public void PlayUiCancel()
    {
        audioSource.PlayOneShot(uiCancel);
    }
}
