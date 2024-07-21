using System;

namespace Core
{
    public class AiWord : ActiveSubjectWord, IComparable
    {
        public AiWord(string name) : base(name)
        {
        }

        public AiWord(string name, Word creator) : base(name, creator)
        {
        }

        public override bool IsAi()
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is AiWord && ((AiWord)obj).name.Equals(this.name);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(name, GetType());
        }

        public override string ToString()
        {
            return $"{name.ToUpper()}";
        }

        public int CompareTo(object obj)
        {
            if (this.GetType() != obj.GetType())
            {
                throw (new ArgumentException(
                       "Both objects being compared must be of type HumanWord."));
            }
            else
            {
                AiWord otherWord = (AiWord)obj;

                if (this == otherWord)
                {
                    return 0;
                }
                else if (MatcherUtils.Matches(this, MatcherWord.AI))
                {
                    return 1;
                }
                else if (MatcherUtils.Matches(otherWord, MatcherWord.AI))
                {
                    return -1;
                }
                else if (MatcherUtils.Matches(this, MatcherWord.ALICE_AI))
                {
                    return 1;
                }
                else if (MatcherUtils.Matches(otherWord, MatcherWord.ALICE_AI))
                {
                    return -1;
                }
                return 0;
            }
        }
    }
}