namespace Puzzles;
public class Pathfinding
{
    public Dictionary<string, Cave> Caves { get; private set; }
    private string VisitedTwice = null;
    private string AllowTwice = "";

    public List<string> Paths = new List<string>();

    public bool AllowRevisit { get; set; } = false;

    public int PathCount { get; private set; } = 0;

    public Pathfinding(string[] input)
    {
        Caves = Cave.Parse(input).ToDictionary(c => c.Id);
    }

    public ICollection<string> GetPaths()
    {
        var start = Caves["start"];
        var visited = new List<string>();
        var paths = new List<string>();
        foreach(var c in Caves.Where(cave => cave.Value.Small && !cave.Value.IsStart && !cave.Value.IsEnd))
        {
            AllowTwice = c.Key;
            var newPaths = PathsFrom(start, visited);
            paths.AddRange(newPaths.Select(p => string.Join(", ", p)));
        }
        //var paths = PathsFrom(start, visited).ToArray();
        paths = paths.Distinct().ToList();
        return paths;
        //return paths.Select(p => string.Join(",", p)).ToArray();
    }

    public IEnumerable<ICollection<string>> PathsFrom(Cave cave, ICollection<string> pathToHere)
    {
        pathToHere.Add(cave.Id);

        // we've reached the end
        if (cave.IsEnd)
        {
            var path = string.Join(",", pathToHere);
            if (Paths.Contains(path)) {
                yield break;
            } else
            {
                PathCount++;
                Paths.Add(path);
                yield return pathToHere;
            }
        }

        foreach (var destId in cave.Destinations)
        {
            var dest = Caves[destId];
            if (dest.Small && pathToHere.Contains(destId)) {
                // do not visit small caves more than once
                if (!AllowRevisit || AllowTwice != destId)
                {
                    continue;
                }
                //if (VisitedTwice != null)
                //{
                //    continue;
                //} else
                //{
                //    VisitedTwice = destId;
                //}
            }

            // enter that cave
            var currentPath = new List<string>(pathToHere); // clone for each sub path

            var paths = PathsFrom(dest, currentPath).ToArray();
            foreach(var path in paths)
            {
                if (path.Contains("end"))
                    yield return path;
            }

            // if the double visit to a small cave was below here, re-allow it
            //if (VisitedTwice != null && !pathToHere.Contains(VisitedTwice))
            //{
            //    VisitedTwice = null;
            //}
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
