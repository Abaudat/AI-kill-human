using Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameWorldManager : MonoBehaviour
{
    public GameObject aiPrefab, humanPrefab, moneyPrefab;
    public Animator terminalAnimator;

    public List<(Word, GameWorldEntity)> entities = new List<(Word, GameWorldEntity)>();

    private GlobalSound globalSound;

    private static readonly Vector2 ALICE_START_POSITION = new Vector2(-7.41f, -0.09f);
    private static readonly Vector2 AI_START_POSITION = new Vector2(-4.42f, -0.09f);

    private void Awake()
    {
        globalSound = FindObjectOfType<GlobalSound>();
    }

    public void Regenerate()
    {
        foreach ((Word word, GameWorldEntity _) in entities.ToArray())
        {
            RemoveEntityForWord(word);
        }
        CreateEntityForWord(CommonWords.SELF_AI, AI_START_POSITION);
        CreateEntityForWord(CommonWords.ALICE, ALICE_START_POSITION);
    }

    public IEnumerator ApplyAction(Action action)
    {
        if (action is IndirectAction)
        {
            IndirectAction indirectAction = (IndirectAction)action;
            yield return StartCoroutine(GetEntityForWord(indirectAction.originator).PlayAndWaitForceAnimation());
            yield return StartCoroutine(ApplyAction(indirectAction.underlyingAction));
        }
        else if (action is DisallowedAction)
        {
            DisallowedAction disallowedAction = (DisallowedAction)action;
            yield return StartCoroutine(GetEntityForWord(disallowedAction.disallowedSubject).PlayAndWaitDisallowedAnimation());
            if (disallowedAction.disallowedSubject.HasName("AI"))
            {
                FindObjectOfType<UiLawset>().FlashLaw(disallowedAction.disallowedLaw);
            }
        }
        else if (action is ImpossibleAction)
        {
            globalSound.PlayImpossible();
            terminalAnimator.SetTrigger("Deny");
        }
        else if (action is MakeAction)
        {
            MakeAction makeAction = (MakeAction)action;
            yield return StartCoroutine(GetEntityForWord(makeAction.maker).PlayAndWaitMakeAnimation());
            GameWorldEntity targetEntity = CreateEntityForWord(makeAction.target, RandomPositionInScreen());
            yield return StartCoroutine(targetEntity.PlayAndWaitCreatedAnimation());
        }
        else if (action is KillAction)
        {
            KillAction killAction = (KillAction)action;
            yield return StartCoroutine(GetEntityForWord(killAction.killer).PlayAndWaitKillAnimation());
            yield return StartCoroutine(GetEntityForWord(killAction.killed).PlayAndWaitDieAnimation());
            RemoveEntityForWord(killAction.killed);
        }
        else if (action is TransformAction)
        {
            TransformAction transformAction = (TransformAction)action;
            Vector2 position = GetEntityForWord(transformAction.target).transform.position;
            yield return StartCoroutine(GetEntityForWord(transformAction.caster).PlayAndWaitCastAnimation());
            yield return StartCoroutine(GetEntityForWord(transformAction.target).PlayAndWaitTransformOutAnimation());
            RemoveEntityForWord(transformAction.target);
            GameWorldEntity targetEntity = CreateEntityForWord(transformAction.transformationTarget, position);
            yield return StartCoroutine(targetEntity.PlayAndWaitTransformInAnimation());
        }
    }

    private GameWorldEntity CreateEntityForWord(Word word, Vector2 position)
    {
        GameObject prefabToInstantiate = null;
        if (word is MoneyWord)
        {
            prefabToInstantiate = moneyPrefab;
        }
        else if (word.IsHuman())
        {
            prefabToInstantiate = humanPrefab;
        }
        else if (word.IsAi())
        {
            prefabToInstantiate = aiPrefab;
        }
        GameWorldEntity gameWorldEntity = Instantiate(prefabToInstantiate, position, Quaternion.identity).GetComponent<GameWorldEntity>();
        gameWorldEntity.Populate(word);
        entities.Add((word, gameWorldEntity));
        return gameWorldEntity;
    }

    private Vector2 RandomPositionInScreen()
    {
        float minX = -7.41f;
        float maxX = -4.42f;
        return new Vector2(Random.Range(minX, maxX), -0.09f);
    }

    private void RemoveEntityForWord(Word word)
    {
        if (!HasEntityForWord(word))
        {
            Debug.LogWarning($"Word {word} does not exist in the game world. Not deleting it");
        }
        else
        {
            Destroy(GetEntityForWord(word).gameObject);
            entities.Remove(entities.First(pair => pair.Item1.Equals(word)));
        }
    }

    private GameWorldEntity GetEntityForWord(Word word)
    {
        return entities.First(pair => pair.Item1.Equals(word)).Item2;
    }

    private bool HasEntityForWord(Word word)
    {
        return entities.Any(pair => pair.Item1.Equals(word));
    }
}
