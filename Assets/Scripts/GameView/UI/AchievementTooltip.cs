using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementTooltip : MonoBehaviour
{
    public TMP_Text titleText, descriptionText;
    public Image icon, backgroundImage;
    public RectTransform mainCanvas;
    public GameObject tooltip;

    public Color unlockedColor, lockedColor;


    public void Show()
    {
        tooltip.SetActive(true);
    }

    public void Hide()
    {
        tooltip.SetActive(false);
    }

    public void PopulateAndMove(string title, string description, Sprite sprite, bool isUnlocked, RectTransform achievementRectTransform)
    {
        titleText.text = title;
        descriptionText.text = description;
        icon.sprite = sprite;
        if (isUnlocked)
        {
            icon.color = unlockedColor;
            backgroundImage.color = unlockedColor;
            titleText.color = unlockedColor;
            descriptionText.color = unlockedColor;
        }
        else
        {
            icon.color = lockedColor;
            backgroundImage.color = lockedColor;
            titleText.color = lockedColor;
            descriptionText.color = lockedColor;
        }
        tooltip.transform.position = ComputeTooltipPosition(achievementRectTransform);
    }

    private Vector2 ComputeTooltipPosition(RectTransform elementRectTransform)
    {
        RectTransform thisRectTransform = tooltip.GetComponent<RectTransform>();
        Vector2 topRightCornerOfElement = new Vector2(elementRectTransform.position.x + elementRectTransform.rect.width / 2, elementRectTransform.position.y + elementRectTransform.rect.height / 2);
        Vector2 idealTooltipPosition = new Vector2(topRightCornerOfElement.x - thisRectTransform.rect.width / 2, topRightCornerOfElement.y + thisRectTransform.rect.height / 2);
        return KeepFullyOnScreen(idealTooltipPosition, thisRectTransform);
    }

    // TODO: Doesn't work for now and not needed
    private Vector2 KeepFullyOnScreen(Vector2 newPos, RectTransform thisRectTransform)
    {
        float minX = (mainCanvas.sizeDelta.x - thisRectTransform.sizeDelta.x) * -0.5f;
        float maxX = (mainCanvas.sizeDelta.x - thisRectTransform.sizeDelta.x) * 0.5f;
        float minY = (mainCanvas.sizeDelta.y - thisRectTransform.sizeDelta.y) * -0.5f;
        float maxY = (mainCanvas.sizeDelta.y - thisRectTransform.sizeDelta.y) * 0.5f;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        return newPos;
    }
}
