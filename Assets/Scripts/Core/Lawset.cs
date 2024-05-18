using System.Linq;

namespace Core
{
    public class Lawset
    {
        public Law[] laws;

        private Lawset(Law[] laws)
        {
            this.laws = laws;
        }

        public bool IsAllowed(Sentence sentence)
        {
            return laws.All(law => law.IsAllowed(sentence));
        }

        public static Lawset Of(params Law[] laws)
        {
            return new Lawset(laws);
        }
    }
}
