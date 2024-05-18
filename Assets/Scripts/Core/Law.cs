using System.Linq;

namespace Core
{
    public class Law
    {
        public MatcherSentence[] disallowedSentences;

        private Law(MatcherSentence[] disallowedSentences)
        {
            this.disallowedSentences = disallowedSentences;
        }

        public bool IsAllowed(Sentence sentence)
        {
            return !disallowedSentences.Any(disallowedSentence => MatcherUtils.Matches(sentence, disallowedSentence));
        }

        public static Law Of(params MatcherSentence[] disallowedSentences)
        {
            return new Law(disallowedSentences);
        }
    }
}