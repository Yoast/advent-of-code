namespace Puzzles;
public class Pathfinding
{
    private Dictionary<string, Cave> Caves;

    public int PathCount { get; private set; } = 0;

    public Pathfinding(string[] input)
    {
        Caves = Cave.Parse(input).ToDictionary(c => c.Id);
    }

    public string[] Paths()
    {
        var start = Caves["start"];
        var visited = new List<string>();

        var paths = PathsFrom(start, visited);
        return paths.Select(p => string.Join(",", p)).ToArray();
    }

    private IEnumerable<List<string>> PathsFrom(Cave cave, ICollection<string> pathToHere)
    {
        pathToHere.Add(cave.Id);

        // we've reached the end
        if (cave.IsEnd)
        {
            PathCount++;
            yield break;
        }

        foreach (var destId in cave.Destinations)
        {
            var dest = Caves[destId];
            if (dest.Small && pathToHere.Contains(destId)) {
                // do not visit small caves more than once
                continue;
            }

            // enter that cave
            var currentPath = new List<string>(pathToHere); // clone for each sub path

            var paths = PathsFrom(dest, currentPath).ToArray();
            foreach(var path in paths)
            {
                if (path.Contains("end"))
                    yield return path;
            }
        }
    }
}

public class Path
{
    public string From { get; }
    public string To { get; }

    public Path(string input, bool reverse = false)
    {
        var parts = input.Split("-");
        var i = reverse ? 1 : 0;
        From = parts[i];
        To = parts[1 - i];
    }
}

public class Cave
{
    public string Id { get; private set; }
    public bool IsStart { get; private set; }
    public bool IsEnd { get; private set; }

    public List<string> Destinations { get; private set; }

    public bool Small => Id.ToLower() == Id;

    public static Cave[] Parse(string[] input)
    {
        var connections = input
            .Select(path => new Path(path))
            .Union(input.Select(path => new Path(path, true)))
            .GroupBy(path => path.From)
            .ToDictionary(paths => paths.Key, paths => paths.ToList()); // every Cave and all its Destinations

        return connections.Select(conn => new Cave
        {
            Id = conn.Key,
            IsStart = conn.Key == "start",
            IsEnd = conn.Key == "end",
            Destinations = conn.Value.Select(d => d.To == conn.Key? d.From : d.To).ToList()
        }).ToArray();
    }

    public override string ToString()
    {
        return $"{Id}=>[{string.Join(",", Destinations)}]";
    }
}
