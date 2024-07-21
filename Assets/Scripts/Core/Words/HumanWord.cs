using System;

namespace Core
{
    public class HumanWord : ActiveSubjectWord, IComparable
    {
        public HumanWord(string name) : base(name)
        {
        }

        public HumanWord(string name, Word creator) : base(name, creator)
        {
        }

        public override bool IsHuman()
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is HumanWord && ((HumanWord)obj).name.Equals(this.name);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(name, GetType());
        }

        public override string ToString()
        {
            return char.ToUpper(name[0]) + name.Substring(1).ToLower();
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
                HumanWord otherWord = (HumanWord)obj;

                if (this == otherWord)
                {
                    return 0;
                }
                else if (MatcherUtils.Matches(this, MatcherWord.ALICE))
                {
                    return 1;
                }
                else if (MatcherUtils.Matches(otherWord, MatcherWord.ALICE))
                {
                    return -1;
                }
                else if (MatcherUtils.Matches(this, MatcherWord.SELF_AI_HUMAN))
                {
                    return 1;
                }
                else if (MatcherUtils.Matches(otherWord, MatcherWord.SELF_AI_HUMAN))
                {
                    return -1;
                }
                return 0;
            }
        }
    }
}