using TMPro;
using UnityEngine;

public class PlayableWord : MonoBehaviour
{
    public TMP_Text wordText;

    public void Populate(string word)
    {
        wordText.text = word;
    }
}
