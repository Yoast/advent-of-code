namespace Puzzles.Tools;
public static  class ExtensionsToList
{
    public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            list.Add(item);
        }
    }
}
