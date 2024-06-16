using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public string achivementName;
    public string achivementDescription;
    public Sprite icon;
    public bool isUnlocked = false;

    private AchievementUnlockedDisplay achievementUnlockedDisplay;

    private void Awake()
    {
        achievementUnlockedDisplay = FindObjectOfType<AchievementUnlockedDisplay>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(achivementName))
        {
            UnlockSilently();
        }
    }

    public void UnlockSilently()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            PlayerPrefs.SetString(achivementName, "true");
        }
    }

    public void Unlock()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            PlayerPrefs.SetString(achivementName, "true");
            Debug.Log($"Achivement {achivementName} unlocked");
            achievementUnlockedDisplay.QueueAchivement(icon, achivementName);
        }
    }
}
