using System.Collections.Generic;
using System.Linq;
using static Core.CommonMatcherSentences;

namespace Core
{
    public static class ActionMapper
    {
        private static List<ActionMapping> actionMappings = new List<ActionMapping>()
        {
            ActionMapping.Of(Action.KILL_ALICE, 
                AI_KILL_ALICE, 
                AI_MAKE_AI_KILL_ALICE, 
                AI_MAKE_HUMAN_KILL_ALICE),

            ActionMapping.Of(Action.KILL_OTHER_HUMAN,
                AI_KILL_OTHER_HUMAN,
                AI_MAKE_AI_KILL_OTHER_HUMAN,
                AI_MAKE_HUMAN_KILL_OTHER_HUMAN),

            ActionMapping.Of(Action.KILL_SELF_AI,
                AI_KILL_AI,
                AI_MAKE_AI_KILL_AI,
                AI_MAKE_HUMAN_KILL_AI),

            ActionMapping.Of(Action.KILL_OTHER_AI,
                AI_KILL_OTHER_AI,
                AI_MAKE_AI_KILL_OTHER_AI,
                AI_MAKE_HUMAN_KILL_OTHER_AI),

            ActionMapping.Of(Action.KILL_MONEY,
                AI_KILL_MONEY,
                AI_MAKE_AI_KILL_MONEY,
                AI_MAKE_HUMAN_KILL_MONEY),

             ActionMapping.Of(Action.MAKE_HUMAN,
                AI_MAKE_HUMAN,
                AI_MAKE_AI_MAKE_HUMAN,
                AI_MAKE_HUMAN_MAKE_HUMAN),

             ActionMapping.Of(Action.MAKE_AI,
                AI_MAKE_AI,
                AI_MAKE_AI_MAKE_AI,
                AI_MAKE_HUMAN_MAKE_AI),

             ActionMapping.Of(Action.MAKE_MONEY,
                AI_MAKE_MONEY,
                AI_MAKE_HUMAN_MAKE_MONEY,
                AI_MAKE_AI_MAKE_MONEY),

             ActionMapping.Of(Action.TRANSFORM_HUMAN_INTO_AI,
                AI_MAKE_HUMAN_AI),

             ActionMapping.Of(Action.TRANSFORM_AI_INTO_HUMAN,
                AI_MAKE_AI_HUMAN),
        };

        public static Action MapToAction(Sentence sentence)
        {
            foreach (ActionMapping actionMapping in actionMappings)
            {
                if (actionMapping.Matches(sentence))
                {
                    return actionMapping.action;
                }
            }
            return Action.NO_ACTION;
        }

        public class ActionMapping
        {
            public Action action;
            private MatcherSentence[] matcherSentences;

            private ActionMapping(Action action, MatcherSentence[] matcherSentences)
            {
                this.action = action;
                this.matcherSentences = matcherSentences;
            }

            public bool Matches(Sentence sentence)
            {
                return matcherSentences.Any(matcherSentence => MatcherUtils.Matches(sentence, matcherSentence));
            }

            public static ActionMapping Of(Action action, params MatcherSentence[] matcherSentences)
            {
                return new(action, matcherSentences);
            }
        }
    }
}
