using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    [Multiline(20)]
    public string text;

    public List<string> GetSentences()
    {
        return text.Split("\n\n")
            .Select(x => x.Trim())
            .Select(x => x.Replace("\n", ""))
            .Where(x => x.Length > 0)
            .ToList();
    }
}
