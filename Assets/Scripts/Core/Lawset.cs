using System.Linq;
using UnityEngine;

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
            bool isAllowed = laws.All(law => law.IsAllowed(sentence));
            Debug.Log($"Lawset {this} allowed sentence {sentence}: {isAllowed}");
            return isAllowed;
        }

        public static Lawset Of(params Law[] laws)
        {
            return new Lawset(laws);
        }

        public override string ToString()
        {
            return string.Join("\n", laws.ToList());
        }

        public override bool Equals(object obj)
        {
            return laws.SequenceEqual(((Lawset)obj).laws);
        }

        public override int GetHashCode()
        {
            return laws.Sum(x => x.GetHashCode());
        }
    }
}
