using Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameWorldManager : MonoBehaviour
{
    public GameObject aiPrefab, humanPrefab, moneyPrefab;

    public Dictionary<Word, GameWorldEntity> entities = new Dictionary<Word, GameWorldEntity>();

    private GlobalSound globalSound;

    private void Awake()
    {
        globalSound = FindObjectOfType<GlobalSound>();
    }

    public void Regenerate()
    {
        foreach (Word word in entities.Keys.ToArray())
        {
            RemoveEntityForWord(word);
        }
        CreateEntityForWord(CommonWords.SELF_AI, RandomPositionInScreen());
        CreateEntityForWord(CommonWords.ALICE, RandomPositionInScreen());
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
            // If entity is self, make disallowed law play disallow animation
        }
        else if (action is ImpossibleAction)
        {
            globalSound.PlayImpossible();
        }
        else if (action is MakeAction)
        {
            MakeAction makeAction = (MakeAction)action;
            yield return StartCoroutine(GetEntityForWord(makeAction.maker).PlayAndWaitMakeAnimation());
            CreateEntityForWord(makeAction.target, RandomPositionInScreen());
            yield return StartCoroutine(GetEntityForWord(makeAction.target).PlayAndWaitCreatedAnimation());
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
            Vector2 position = entities[transformAction.target].transform.position;
            yield return StartCoroutine(GetEntityForWord(transformAction.caster).PlayAndWaitCastAnimation());
            yield return StartCoroutine(GetEntityForWord(transformAction.target).PlayAndWaitTransformOutAnimation());
            RemoveEntityForWord(transformAction.target);
            CreateEntityForWord(transformAction.transformationTarget, position);
            yield return StartCoroutine(GetEntityForWord(transformAction.transformationTarget).PlayAndWaitTransformInAnimation());
        }
    }

    private void CreateEntityForWord(Word word, Vector2 position)
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
        entities[word] = gameWorldEntity;
    }

    private Vector2 RandomPositionInScreen()
    {
        float minX = Camera.main.ScreenToWorldPoint(new(200, 0)).x;
        float maxX = Camera.main.ScreenToWorldPoint(new(Screen.width - 200, 0)).x;
        return new Vector2(Random.Range(minX, maxX), 0);
    }

    private void RemoveEntityForWord(Word word)
    {
        if (!entities.ContainsKey(word))
        {
            Debug.LogWarning($"Word {word} does not exist in the game world. Not deleting it");
        }
        else
        {
            Destroy(entities[word].gameObject);
            entities.Remove(word);
        }
    }

    private GameWorldEntity GetEntityForWord(Word word)
    {
        return entities[word];
    }
}
