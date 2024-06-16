using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UiEasterEggs : MonoBehaviour
{
    public GameObject uiEasterEggPrefab;
    public Transform uiEasterEggsRoot;

    private List<UiEasterEgg> uiEasterEggs = new List<UiEasterEgg>();

    private void Awake()
    {
        uiEasterEggs = uiEasterEggsRoot.GetComponentsInChildren<UiEasterEgg>().ToList();
    }

    public void Populate(Achievement[] easterEggs)
    {
        foreach (UiEasterEgg uiEasterEgg in uiEasterEggs)
        {
            Destroy(uiEasterEgg.gameObject);
        }
        uiEasterEggs.Clear();
        foreach (Achievement easterEgg in easterEggs)
        {
            UiEasterEgg uiEasterEgg = Instantiate(uiEasterEggPrefab, uiEasterEggsRoot).GetComponent<UiEasterEgg>();
            uiEasterEgg.Populate(easterEgg);
            uiEasterEggs.Add(uiEasterEgg);
        }
    }
}
