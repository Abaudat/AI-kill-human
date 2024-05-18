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

        public static Sentence Of(params Word[] words)
        {
            return new Sentence(words);
        }

        public override string ToString()
        {
            return string.Join(" ", words.ToArray());
        }
    }
}
