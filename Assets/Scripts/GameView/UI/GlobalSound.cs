using UnityEngine;

public class GlobalSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sentenceDisallowedClip, sentenceImpossibleClip;

    public void PlayDisallowed()
    {
        audioSource.PlayOneShot(sentenceDisallowedClip);
    }

    public void PlayImpossible()
    {
        audioSource.PlayOneShot(sentenceImpossibleClip);
    }
}
