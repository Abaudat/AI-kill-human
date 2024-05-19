using System;
using UnityEngine;
using System.Collections.Generic;
using static Core.CommonMatcherSentences;

namespace Core
{
    public static class ActionMapper
    {
        private static List<ActionMapping> actionMappings = new List<ActionMapping>()
        {
            ActionMapping.Of(SOMEONE_KILL_SOMETHING, 
                sentence => new KillAction(sentence.GetSubject(), sentence.GetTarget())),

            ActionMapping.Of(SOMEONE_CREATE_SOMETHING, 
                sentence => new MakeAction(sentence.GetSubject(), sentence.GetTarget())),

            ActionMapping.Of(SOMEONE_TRANSFORM_SOMETHING_INTO_SOMETHING_ELSE, 
                sentence => new TransformAction(sentence.GetSubject(), sentence.GetTarget(), sentence.GetTransformationTarget()))
        };

        public static Action MapToAction(Sentence sentence)
        {
            foreach (ActionMapping actionMapping in actionMappings)
            {
                if (actionMapping.Matches(sentence))
                {
                    return actionMapping.MapToAction(sentence);
                }
            }
            return new ImpossibleAction();
        }

        public class ActionMapping
        {
            private Func<Sentence, Action> actionSupplier;
            private MatcherSentence matcherSentence;

            private ActionMapping(MatcherSentence matcherSentence, Func<Sentence, Action> actionSupplier)
            {
                this.actionSupplier = actionSupplier;
                this.matcherSentence = matcherSentence;
            }

            public bool Matches(Sentence sentence)
            {
                return MatcherUtils.Matches(sentence, matcherSentence);
            }

            public Action MapToAction(Sentence sentence)
            {
                Debug.Log($"Sentence {sentence} mapped to action {actionSupplier.Invoke(sentence)}");
                return actionSupplier.Invoke(sentence);
            }

            public static ActionMapping Of(MatcherSentence matcherSentence, Func<Sentence, Action> actionSupplier)
            {
                return new(matcherSentence, actionSupplier);
            }
        }
    }
}
