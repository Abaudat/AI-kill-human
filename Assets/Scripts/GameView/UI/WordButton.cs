using Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Word word;
    public TMP_Text wordText;
    public Transform child;

    private CurrentSentenceManager currentSentenceManager;

    private void Awake()
    {
        currentSentenceManager = FindObjectOfType<CurrentSentenceManager>();
    }

    public void Populate(Word word)
    {
        this.word = word;
        this.wordText.text = word.ToString();
    }

    public void PlayWord()
    {
        currentSentenceManager.AppendWord(word);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
        child.position = Vector2.Lerp(child.position, Input.mousePosition, Time.deltaTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
        child.position = Vector2.zero;
    }
}
