using TMPro;
using UnityEngine;

public class TerminalWord : MonoBehaviour
{
    public TMP_Text wordText;

    public void Populate(string word)
    {
        wordText.text = word;
    }
}
