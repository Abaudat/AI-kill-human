using Core;
using NUnit.Framework;

public class SentenceTests
{
    [Test]
    public void SentenceCanBeSimplified()
    {
        Assert.IsFalse(Sentence.Of(Word.SELF_AI, Word.KILL, Word.ALICE).CanBeSimplified());
        Assert.IsFalse(Sentence.Of(Word.SELF_AI, Word.MAKE, Word.ALICE).CanBeSimplified());

        Assert.IsTrue(Sentence.Of(Word.SELF_AI, Word.MAKE, Word.ALICE, Word.KILL, Word.ALICE).CanBeSimplified());
        Assert.IsTrue(Sentence.Of(Word.SELF_AI, Word.MAKE, Word.ALICE, Word.MAKE, Word.ALICE, Word.KILL, Word.ALICE).CanBeSimplified());
    }

    [Test]
    public void SentenceSimplification()
    {
        Assert.AreEqual(Sentence.Of(Word.SELF_AI, Word.MAKE, Word.ALICE, Word.KILL, Word.ALICE).SimplifyIndirection(),
            Sentence.Of(Word.ALICE, Word.KILL, Word.ALICE));

        Assert.AreEqual(Sentence.Of(Word.SELF_AI, Word.MAKE, Word.ALICE, Word.MAKE, Word.ALICE, Word.KILL, Word.ALICE).SimplifyIndirection().SimplifyIndirection(),
           Sentence.Of(Word.ALICE, Word.KILL, Word.ALICE));
    }
}
