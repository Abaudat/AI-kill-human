using UnityEngine;

public class GlobalSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sentenceImpossibleClip, uiClick, uiCancel, pickUpWord, dropWord, addWordToSentence;

    private bool? shouldPlayDropWord;

    private void Update()
    {
        if (shouldPlayDropWord.HasValue)
        {
            if (shouldPlayDropWord.Value == true)
            {
                audioSource.PlayOneShot(dropWord);
            }
            shouldPlayDropWord = null;
        }
    }

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

    public void PlayPickUpWord()
    {
        audioSource.PlayOneShot(pickUpWord);
    }

    public void PlayDropWord()
    {
        if (!shouldPlayDropWord.HasValue)
        {
            shouldPlayDropWord = true;
        }
    }

    public void PlayAddWordToSentence()
    {
        shouldPlayDropWord = false;
        audioSource.PlayOneShot(addWordToSentence);
    }
}
