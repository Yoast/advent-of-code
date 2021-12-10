using Puzzles.Tools;
using System.Drawing;
using System.Linq;

namespace Puzzles;

public class HeatMap
{
	protected Matrix2d<int> Map;
	public HeatMap(string[] input)
	{
		Map = new Matrix2d<int>(input[0].Length, input.Length);
		var y = 0;
		foreach (var line in input)
		{
			var x = 0;
			foreach( var i in line) {
				Map[x++, y] = int.Parse(i.ToString());
			}
			y++;
		}
		Console.WriteLine(Map.ToString());
	}

	public IEnumerable<int> DeepestLevels()
	{
		return DeepestLocations().Select(p => Map[p]);
	}

	public IEnumerable<Point> DeepestLocations() { 
		for (var x = 0; x < Map.Width; x++)
		{
			for (var y = 0; y < Map.Height; y++)
			{
				Console.Write(Map[x,y]);
				var adj = Map.AdjacentValues(x, y);
				if (adj.All(v => v > Map[x,y]))
				{
					Console.Write('<');
					yield return new Point(x,y);
				} else { 
					Console.Write(' ');
				}
			}
			Console.WriteLine();
		}
	}

	public int Basins()
	{
		var deepestPoints = DeepestLocations();
		List<int> basins = new List<int>();
		foreach(var z in deepestPoints)
        {
			basins.Add(GrowBasin(z).ToArray().Length);
        }

		var result = 1;
		foreach(var z in basins.OrderByDescending(x => x).Take(3))
        {
			result *= z;
        }
		return result;
	}

	public IEnumerable<Point> GrowBasin(Point source)
    {
		if (Map[source] == 9)
		{
			return new Point[0];
		}

		var output = new List<Point> { source };
		var basin = Map.AdjacentPoints(source).InBasin(Map).ToArray();
		output.AddRange(basin);

		var oldBasin = basin;

		do
		{
			oldBasin = basin;
			basin = oldBasin.SelectMany(p => Map.AdjacentPoints(p)).ToArray();
			basin = basin.Where(p => !output.Contains(p)).ToArray(); // do not recheck known points
			basin = basin.InBasin(Map).ToArray();

			if (basin.Length > 0 && oldBasin != basin)
			{
				output.AddRange(basin);
			}
		} while (basin.Length > 0 && oldBasin != basin);
		return output;	
    }
}
