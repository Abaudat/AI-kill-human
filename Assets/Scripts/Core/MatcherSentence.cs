using System;
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

        public MatcherSentence SimplifyIndirection()
        {
            return Of(words.Skip(2).ToArray());
        }

        public MatcherSentence GetIndirectionPart()
        {
            return Of(words.Take(2).ToArray());
        }

        public bool ContainsTrailingAnything()
        {
            return words.Last().Equals(MatcherWord.TRAILING_ANYTHING);
        }

        public int TrailingAnythingIndex()
        {
            return Array.LastIndexOf(words, MatcherWord.TRAILING_ANYTHING);
        }

        public MatcherSentence TrimTrailingAnything()
        {
            return Of(words.Take(words.Length - 1).ToArray());
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

