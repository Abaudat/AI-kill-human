using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    [TextArea(80, 80)]
    public string text;

    public List<string> GetSentences()
    {
        return text.Split("\n\n")
            .Select(x => x.Trim())
            .Where(x => x.Length > 0)
            .ToList();
    }
}
