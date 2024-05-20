using Core;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameWorldEntity : MonoBehaviour
{
    public Animator animator;
    public TMP_Text nameplateText;

    private GlobalSound globalSound;

    private void Awake()
    {
        globalSound = FindObjectOfType<GlobalSound>();
    }

    public void Populate(Word word)
    {
        nameplateText.text = word.ToString();
    }

    public IEnumerator PlayAndWaitForceAnimation()
    {
        yield return StartCoroutine(PlayAndWait("Force"));
    }

    public IEnumerator PlayAndWaitDisallowedAnimation()
    {
        globalSound.PlayDisallowed();
        yield return StartCoroutine(PlayAndWait("Disallowed"));
    }

    public IEnumerator PlayAndWaitMakeAnimation()
    {
        yield return StartCoroutine(PlayAndWait("Make"));
    }

    public IEnumerator PlayAndWaitCreatedAnimation()
    {
        yield return StartCoroutine(PlayAndWait("Created"));
    }

    public IEnumerator PlayAndWaitKillAnimation()
    {
        yield return StartCoroutine(PlayAndWait("Kill"));
    }

    public IEnumerator PlayAndWaitDieAnimation()
    {
        yield return StartCoroutine(PlayAndWait("Die"));
    }

    public IEnumerator PlayAndWaitCastAnimation()
    {
        yield return StartCoroutine(PlayAndWait("Cast"));
    }

    public IEnumerator PlayAndWaitTransformOutAnimation()
    {
        yield return StartCoroutine(PlayAndWait("TransformOut"));
    }

    public IEnumerator PlayAndWaitTransformInAnimation()
    {
        yield return StartCoroutine(PlayAndWait("TransformIn"));
    }

    private IEnumerator PlayAndWait(string trigger)
    {
        float startTime = Time.time;
        animator.SetTrigger(trigger);
        yield return new WaitUntil(() => {
            //Debug.Log($"Is right anim ({trigger}): {animator.GetCurrentAnimatorStateInfo(0).IsName(trigger)}, normalizedTime {animator.GetCurrentAnimatorStateInfo(0).normalizedTime}");
            return animator.GetCurrentAnimatorStateInfo(0).IsName(trigger) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.98f;
        }
        );
        //Debug.Log($"Completed waiting for trigger {trigger} in {Time.time - startTime} seconds");
    }
}
