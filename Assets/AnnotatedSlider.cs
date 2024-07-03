using TMPro;
using UnityEngine;

public class AnnotatedSlider : MonoBehaviour
{
    public TMP_Text annotation;

    public void PropagateValueToAnnotation(float value)
    {
        annotation.text = Mathf.RoundToInt(value).ToString();
    }
}
