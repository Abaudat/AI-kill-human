using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundVolumeSliderTrigger : MonoBehaviour, IPointerUpHandler
{
    public AudioSource audioSource;
    public AudioClip testAudioClip;

    public void OnPointerUp(PointerEventData eventData)
    {
        audioSource.PlayOneShot(testAudioClip);
    }
}
