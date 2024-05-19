public static class AiNameGenerator
{
    private static int counter = 0;

    private static string[] names = new string[]
    {
        "BETA",
        "C-BORG",
        "DELTOID",
        "ECHO",
        "FOX"
    };

    public static string GenerateAiName()
    {
        return names[counter++ % names.Length];
    }

    public static void ResetCounter()
    {
        counter = 0;
    }
}