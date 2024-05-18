using System.Linq;
using UnityEngine;

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
            bool isAllowed = !disallowedSentences.Any(disallowedSentence => MatcherUtils.Matches(sentence, disallowedSentence));
            Debug.Log($"Law {this} allowed sentence {sentence}: {isAllowed}");
            return isAllowed;
        }

        public static Law Of(params MatcherSentence[] disallowedSentences)
        {
            return new Law(disallowedSentences);
        }

        public override string ToString()
        {
            return string.Join(", ", disallowedSentences.ToList());
        }

        public override bool Equals(object obj)
        {
            return disallowedSentences.SequenceEqual(((Law)obj).disallowedSentences);
        }

        public override int GetHashCode()
        {
            return disallowedSentences.Sum(x => x.GetHashCode());
        }
    }
}