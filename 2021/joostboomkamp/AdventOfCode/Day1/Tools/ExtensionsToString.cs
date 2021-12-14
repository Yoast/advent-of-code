namespace Puzzles.Tools;

public static class ExtensionsToString
{
    public static string[] SplitByLine(this string input, StringSplitOptions options = StringSplitOptions.None)
    {
        return input.ReplaceLineEndings().Split(Environment.NewLine, options);
    }

    public static ICollection<string> ToPairs(this string input)
    {
        var output = new List<string>();
        for (var i = 1; i < input.Length; i++)
        {
            output.Add(input.Substring(i - 1, 2));
        }
        return output;
    }
}
