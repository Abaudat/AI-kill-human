using System.Linq;

namespace Core
{
    public class Sentence
    {
        public Word[] words;

        private Sentence(Word[] words)
        {
            this.words = words;
        }

        public bool CanBeSimplified()
        {
            return words.Length >= 4 && words[0].IsActiveSubject() && words[1].IsMake() && words[2].IsActiveSubject() && words[3].IsVerb();
        }

        public Sentence SimplifyIndirection()
        {
            if (words[0].IsActiveSubject() && words[1].IsMake())
            {
                return Sentence.Of(words.Skip(2).ToArray());
            }
            else return this;
        }

        public Word GetSubject()
        {
            return words[0];
        }

        public Word GetTarget()
        {
            return words[2];
        }

        public Word GetTransformationTarget()
        {
            return words[3];
        }

        public static Sentence Of(params Word[] words)
        {
            return new Sentence(words);
        }

        public override string ToString()
        {
            return string.Join(" ", words.ToArray());
        }

        public override bool Equals(object obj)
        {
            return words.SequenceEqual(((Sentence)obj).words);
        }
    }
}
