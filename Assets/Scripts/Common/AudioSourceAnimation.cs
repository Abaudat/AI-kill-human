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
        if (PlayerPrefs.HasKey("soundVolume"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("soundVolume");
        }
        audioSource.PlayOneShot(audioClip);
    }
}
