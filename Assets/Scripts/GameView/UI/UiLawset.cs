using Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UiLawset : MonoBehaviour
{
    public GameObject lawUiPrefab;
    public Transform lawUiRoot;
    private Dictionary<Law, UiLaw> lawToUiMapping = new Dictionary<Law, UiLaw>();

    public void Repopulate(Lawset lawset)
    {
        foreach (Law law in lawToUiMapping.Keys.ToArray())
        {
            if (!lawset.laws.Contains(law))
            {
                Destroy(lawToUiMapping[law]);
                lawToUiMapping.Remove(law);
            }
        }
        foreach (Law law in lawset.laws)
        {
            if (!lawToUiMapping.ContainsKey(law))
            {
                UiLaw uiLaw = Instantiate(lawUiPrefab, lawUiRoot).GetComponent<UiLaw>();
                uiLaw.Populate(LawTranslator.GetLawInPlaintext(law));
                lawToUiMapping.Add(law, uiLaw);
            }
        }
    }

    public void FlashLaw(Law law)
    {
        if (!lawToUiMapping.ContainsKey(law))
        {
            Debug.LogError($"UI laws do not contain the law {law}. Not flashing it");
            return;
        }
        lawToUiMapping[law].Flash();
    }
}
