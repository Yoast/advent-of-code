namespace Puzzles;

public static class ExtToIntArrays
	{
		public static IEnumerable<int> AdjacentValues(this int[][] map, int x, int y)
		{
			if (x > 0)
			{
				yield return map[y][x - 1];
			}

			if (y > 0)
			{
				yield return map[y - 1][x];
			}

			if (x < map[y].Length - 1)
			{
				yield return map[y][x + 1];
			}

			if (y < map.Length - 1)
			{
				yield return map[y + 1][x];
			}
		}

	public static int Risk(this int[] source)
    {
		return source.Sum(x => x + 1);
    }
}
