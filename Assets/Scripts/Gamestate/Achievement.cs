using UnityEngine;

public class Achievement : MonoBehaviour
{
    public string achivementName;
    public string achivementDescription;
    public Sprite icon;
    public Dialogue dialogue;
    public bool isUnlocked = false;

    private AchievementUnlockedDisplay achievementUnlockedDisplay;
    private ProgressPanelManager progressPanelManager;

    private void Awake()
    {
        achievementUnlockedDisplay = FindObjectOfType<AchievementUnlockedDisplay>();
        progressPanelManager = FindObjectOfType<ProgressPanelManager>();
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

    public void UnlockWithNotification()
    {
        if (!isUnlocked)
        {
            progressPanelManager.DisplayNotification();
            UnlockSilently();
        }
    }

    public void Unlock()
    {
        if (!isUnlocked)
        {
            UnlockWithNotification();
            achievementUnlockedDisplay.QueueAchivement(icon, achivementName);
        }
    }
}
