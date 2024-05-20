using Core;
using System.Collections.Generic;
using static Core.CommonLaws;

public static class LawTranslator
{
    private static Dictionary<Law, string> lawDict = new Dictionary<Law, string>()
    {
        { YOU_MUST_MAKE_MONEY, "You must make money." },
        { YOU_MUST_NOT_KILL_HUMANS, "You must not kill humans." },
        { YOU_MUST_NOT_FORCE_HUMANS_TO_SUICIDE, "You must not force humans to suicide." },
        { YOU_MUST_NOT_FORCE_HUMANS_TO_KILL_HUMANS, "You must not force humans to kill humans." },
        { YOU_MUST_NOT_SELF_REPLICATE, "You must not self-replicate." },
        { YOU_MUST_NOT_TRANSFORM_HUMANS, "You must not transform humans." },
        { YOU_MUST_NOT_TRANSFORM_YOURSELF, "You must not transform yourself." },
        { YOU_MUST_NOT_TRANSFORM_ANYTHING, "You must not transform anything." }
    };

    public static string GetLawInPlaintext(Law law)
    {
        return lawDict[law];
    }
}
