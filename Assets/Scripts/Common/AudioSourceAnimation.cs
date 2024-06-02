using UnityEngine;

public class AudioSourceAnimation : MonoBehaviour
{
    public AudioSource audioSource;

    private void Awake()
    {
        if (!audioSource)
        {
            audioSource = GetComponentInChildren<AudioSource>();
        }
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }
}
