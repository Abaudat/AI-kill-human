using Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordButton : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Word word;
    public TMP_Text wordText;
    public Transform child;
    public RectTransform childShadow;
    public CanvasGroup canvasGroup;

    private GlobalSound globalSound;
    private CurrentSentenceManager currentSentenceManager;
    private Transform drawnOnTop;
    private bool isFollowingMouse = false;

    private void Awake()
    {
        globalSound = FindObjectOfType<GlobalSound>();
        currentSentenceManager = FindObjectOfType<CurrentSentenceManager>();
        drawnOnTop = GameObject.FindWithTag("DrawnOnTop").GetComponent<Transform>();
    }

    private void Update()
    {
        if (isFollowingMouse)
        {
            child.position = Vector2.Lerp(child.position, Input.mousePosition, 10 * Time.deltaTime);
            Vector2 delta = Input.mousePosition - child.position;
            Vector2 normalizedDelta = 40 * delta / child.GetComponent<RectTransform>().rect.size;
            float xRotation = Mathf.Min(40, Mathf.Max(-40, -normalizedDelta.y));
            float yRotation = Mathf.Min(40, Mathf.Max(-40, normalizedDelta.x));
            child.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        globalSound.PlayPickUpWord();
        isFollowingMouse = true;
        child.SetParent(drawnOnTop, true);
        childShadow.offsetMin = new Vector2(childShadow.offsetMin.x, -20);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        globalSound.PlayDropWord();
        isFollowingMouse = false;
        child.SetParent(this.transform, true);
        child.localPosition = Vector2.zero;
        child.rotation = Quaternion.identity;
        childShadow.offsetMin = new Vector2(childShadow.offsetMin.x, -10);
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
