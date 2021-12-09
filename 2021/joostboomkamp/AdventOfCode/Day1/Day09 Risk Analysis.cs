namespace Puzzles;

public class HeatMap
{
	protected int[][] Map;
	public HeatMap(string[] input)
	{
		Map = new int[input.Length][];
		var y = 0;
		foreach (var line in input)
		{
			Map[y] = line.Select(c => int.Parse(c.ToString())).ToArray();
			y++;
		}
		Console.WriteLine(Map[0].Length.ToString() + "x" + Map.Length.ToString());
	}

	public IEnumerable<int> CoolestSpots()
	{
		// 2199943210
		// 3987894921
		// 9856789892
		// 8767896789
		// 9899965678
		for (var y = 0; y < Map.Length; y++)
		{
			for (var x = 0; x < Map[y].Length; x++)
			{
				Console.Write(Map[y][x]);
				var adj = Map.AdjacentValues(x, y);
				if (adj.All(v => v > Map[y][x]))
				{
					Console.Write('<');
					yield return Map[y][x];
				} else { 
					Console.Write(' ');
				}
			}
			Console.WriteLine();
		}
	}
}
