using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Achievement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string achivementName;
    public string achivementDescription;

    public Image image, backgroundImage;

    private AchievementTooltip tooltip;
    private AchievementUnlockedDisplay achievementUnlockedDisplay;

    private bool isUnlocked = false;
    private bool isHovering = false;
    private float hoverTime = 0;

    private void Awake()
    {
        tooltip = FindObjectOfType<AchievementTooltip>();
        achievementUnlockedDisplay = FindObjectOfType<AchievementUnlockedDisplay>();
        image.color = Color.black;
        backgroundImage.color = Color.black;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(achivementName))
        {
            Unlock(false);
        }
    }

    private void Update()
    {
        if (isHovering)
        {
            hoverTime += Time.deltaTime;
        }
        if (hoverTime > 0.5f)
        {
            tooltip.gameObject.SetActive(true);
            tooltip.PopulateAndMove(achivementName, isUnlocked ? achivementDescription : "???", image.sprite, isUnlocked, GetComponent<RectTransform>());
        }
    }

    public void Unlock(bool notify = true)
    {
        if (!isUnlocked)
        {
            Debug.Log($"Achivement {achivementName} unlocked");
            image.color = Color.white;
            backgroundImage.color = Color.white;
            isUnlocked = true;
            PlayerPrefs.SetString(achivementName, "true");
            if (notify)
            {
                achievementUnlockedDisplay.QueueAchivement(image.sprite, achivementName);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        hoverTime = 0;
        tooltip.gameObject.SetActive(false);
    }
}
