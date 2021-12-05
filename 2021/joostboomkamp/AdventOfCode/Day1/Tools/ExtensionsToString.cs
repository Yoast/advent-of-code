namespace Puzzles.Tools;

public static class ExtensionsToString
{
    public static string[] SplitByLine(this string input, StringSplitOptions options = StringSplitOptions.None)
    {
        return input.ReplaceLineEndings().Split(Environment.NewLine, options);
    }
}
