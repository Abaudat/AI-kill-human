using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnnotatedProgressBar : MonoBehaviour
{
    public GameObject stepPrefab;
    public Transform stepsRoot;
    public TMP_Text annotation;
    public int max, current;
    public Color filledColor, emptyColor;
    public float animationInterval = 1, animationLoopDelay = 10;

    private List<Image> steps = new List<Image>();
    private int animationIndex = 0;

    private void Awake()
    {
        steps = GetComponentsInChildren<Image>().ToList();
    }

    private void Start()
    {
        StartCoroutine(Animate());
    }

    public void SetProgress(int current, int max)
    {
        this.max = max;
        this.current = current;
        Refresh();
    }

    private void Refresh()
    {
        foreach (Image step in steps)
        {
            Destroy(step.gameObject);
        }
        steps.Clear();
        for (int i = 0; i < max; i++)
        {
            GameObject step = Instantiate(stepPrefab, stepsRoot);
            steps.Add(step.GetComponent<Image>());
        }
        foreach (Image step in steps)
        {
            step.color = emptyColor;
        }
        foreach (Image step in steps.Take(current))
        {
            step.color = filledColor;
        }
        annotation.text = $"{current}/{max}";
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            if (steps.Count <= 0)
            {
                yield return new WaitForSeconds(animationInterval);
            }
            if (animationIndex >= steps.Count || animationIndex >= current)
            {
                animationIndex = 0;
                yield return new WaitForSeconds(animationLoopDelay);
            }
            steps[animationIndex].GetComponent<Animator>().SetTrigger("Grow");
            animationIndex++;
            yield return new WaitForSeconds(animationInterval);
        }
    }
}
