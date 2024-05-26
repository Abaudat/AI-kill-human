using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementTooltip : MonoBehaviour
{
    public TMP_Text titleText, descriptionText;
    public Image image, backgroundImage;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void PopulateAndMove(string title, string description, Sprite sprite, bool isUnlocked, Vector2 position)
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
        // TODO: Move
    }
}
