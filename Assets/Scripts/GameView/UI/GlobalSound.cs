using UnityEngine;

public class GlobalSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sentenceImpossibleClip;

    public void PlayImpossible()
    {
        audioSource.PlayOneShot(sentenceImpossibleClip);
    }
}
