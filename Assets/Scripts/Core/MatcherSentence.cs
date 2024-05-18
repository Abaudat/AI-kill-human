using System.Linq;

namespace Core
{
    public class MatcherSentence
    {
        public MatcherWord[] words;

        private MatcherSentence(MatcherWord[] words)
        {
            this.words = words;
        }

        public static MatcherSentence Of(params MatcherWord[] words)
        {
            return new MatcherSentence(words);
        }

        public override string ToString()
        {
            return string.Join(" ", words);
        }

        public override bool Equals(object obj)
        {
            return words.SequenceEqual(((MatcherSentence)obj).words);
        }

        public override int GetHashCode()
        {
            return words.Sum(x => x.GetHashCode());
        }
    }
}

