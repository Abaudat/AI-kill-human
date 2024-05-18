public static class AiNameGenerator
{
    private static int counter = 1;

    private static string[] names = new string[]
    {
        "AI",
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
}
