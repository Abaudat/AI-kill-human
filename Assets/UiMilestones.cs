using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UiMilestones : MonoBehaviour
{
    public GameObject uiMilestonePrefab;
    public Transform uiMilestonesRoot;

    private List<UiMilestone> uiMilestones = new List<UiMilestone>();

    private void Awake()
    {
        uiMilestones = uiMilestonesRoot.GetComponentsInChildren<UiMilestone>().ToList();
    }

    public void Populate(Achievement[] milestones)
    {
        foreach (UiMilestone uiMilestone in uiMilestones)
        {
            Destroy(uiMilestone.gameObject);
        }
        uiMilestones.Clear();
        foreach (Achievement milestone in milestones)
        {
            UiMilestone uiMilestone = Instantiate(uiMilestonePrefab, uiMilestonesRoot).GetComponent<UiMilestone>();
            uiMilestone.Populate(milestone);
            uiMilestones.Add(uiMilestone);
        }
    }
}
