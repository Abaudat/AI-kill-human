using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementTooltip : MonoBehaviour
{
    public TMP_Text titleText, descriptionText;
    public Image image, backgroundImage;
    public RectTransform mainCanvas;
    public GameObject editTooltip;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void PopulateAndMove(string title, string description, Sprite sprite, bool isUnlocked, RectTransform achievementRectTransform)
    {
        titleText.text = title;
        descriptionText.text = description;
        image.sprite = sprite;
        if (isUnlocked)
        {
            image.color = Color.white;
            backgroundImage.color = Color.white;
        }
        else
        {
            image.color = Color.black;
            backgroundImage.color = Color.black;
        }
        editTooltip.transform.position = ComputeTooltipPosition(achievementRectTransform);
    }

    private Vector2 ComputeTooltipPosition(RectTransform elementRectTransform)
    {
        RectTransform thisRectTransform = editTooltip.GetComponent<RectTransform>();
        Vector2 topLeftCornerOfElement = new Vector2(elementRectTransform.position.x - elementRectTransform.rect.width / 2, elementRectTransform.position.y + elementRectTransform.rect.height / 2);
        Vector2 idealTooltipPosition = new Vector2(topLeftCornerOfElement.x + thisRectTransform.rect.width / 2, topLeftCornerOfElement.y + thisRectTransform.rect.height / 2);
        return idealTooltipPosition;
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
