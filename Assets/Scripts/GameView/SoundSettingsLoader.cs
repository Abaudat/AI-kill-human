using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundSettingsLoader : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("soundVolume"))
        {
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("soundVolume");
        }
    }
}
