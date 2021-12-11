using System.Drawing;

namespace Puzzles.Tools;

public static class ExtToIntArrays
{
	public static IEnumerable<T> AdjacentValues<T>(this Matrix2d<T> map, int x, int y)
	{
		return map.AdjacentPoints(x, y).Select(p => map[p]);
	}

	public static IEnumerable<Point> AdjacentPoints<T>(this Matrix2d<T> map, Point p)
		=> map.AdjacentPoints(p.X, p.Y);

	public static IEnumerable<Point> AdjacentPoints<T>(this Matrix2d<T> map, int x, int y)
	{
		if (x > 0)
		{
			yield return new Point(x - 1, y);
		}

		if (y > 0)
		{
			yield return new Point(x, y - 1);
		}

		if (x < map.Width - 1)
		{
			yield return new Point(x + 1, y);
		}

		if (y < map.Height - 1)
		{
			yield return new Point(x, y + 1);
		}
	}

	public static IEnumerable<Point> SurroundingPoints<T>(this Matrix2d<T> map, Point p)
	{
		return SurroundingPoints(map, p.X, p.Y);
	}

	public static IEnumerable<Point> SurroundingPoints<T>(this Matrix2d<T> map, int x, int y)
	{
		foreach (var p in map.AdjacentPoints(x, y))
		{
			yield return p;
		}

		if (y > 0) { 

			if (x > 0)
			{
				yield return new Point(x - 1, y - 1);
			}

			if (x < map.Width - 1)
			{
				yield return new Point(x + 1, y - 1);
			}
		}

		if (y < map.Height - 1)
		{
			if (x > 0)
			{
				yield return new Point(x - 1, y + 1);
			}
			if (x < map.Width - 1)
			{
				yield return new Point(x + 1, y + 1);
			}
		}
	}

	public static int Risk(this int[] source)
    {
		return source.Sum(x => x + 1);
    }

	public static IEnumerable<Point> InBasin(this IEnumerable<Point> source, Matrix2d<int> map)
	{
		return source.Where(p => map[p] < 9).Distinct();
	}
}
