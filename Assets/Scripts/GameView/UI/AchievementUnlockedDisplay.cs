using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUnlockedDisplay : MonoBehaviour
{
    public GameObject achivementUnlockedPanel;
    public Image image;
    public TMP_Text achievementNameText;

    Queue<(Sprite, string)> achievementsToUnlock = new Queue<(Sprite, string)>();

    private float currentAchivementDisplayTime = 0;

    private void Update()
    {
        currentAchivementDisplayTime -= Time.deltaTime;
        if (currentAchivementDisplayTime <= 0)
        {
            if (achievementsToUnlock.Count > 0)
            {
                achivementUnlockedPanel.SetActive(true);
                (Sprite sprite, string name) = achievementsToUnlock.Dequeue();
                image.sprite = sprite;
                achievementNameText.text = name;
                currentAchivementDisplayTime = 2;
            }
            else
            {
                achivementUnlockedPanel.SetActive(false);
            }
        }
    }

    public void QueueAchivement(Sprite sprite, string name)
    {
        achievementsToUnlock.Enqueue((sprite, name));
    }
}
