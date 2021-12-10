using Puzzles.Tools;
using System.Drawing;

namespace Puzzles;

public class HydrothermalVents
{
    private Matrix2d<int> _map;

    private ICollection<VentLine> _lines;

    public HydrothermalVents(string[] input, bool excludeDiagonals = true)
    {
        var lines = input.Select(VentLine.Parse);
        if (excludeDiagonals)
        {
            lines = lines.Where(l => !l.IsDiagonal);
        }
        _lines = lines.ToArray();

        var upperLeftX = _lines.Min(line => line.Points.Min(point => point.X));
        var upperLeftY = _lines.Min(line => line.Points.Min(point => point.Y));
        var LowerRightX = _lines.Max(line => line.Points.Max(point => point.X));
        var LowerRightY = _lines.Max(line => line.Points.Max(point => point.Y));

        _map = new Matrix2d<int>(LowerRightX - upperLeftX + 1, LowerRightY - upperLeftY + 1);
        _map.OffsetX = -upperLeftX;
        _map.OffsetY = -upperLeftY;

        foreach(var line in _lines)
        {
            foreach(var point in line.Points)
            {
                _map[point.X, point.Y] += 1;
            }
        }
    }

    public int CountRiskyZones => _map.Count(p => p >= 2);

}

public class VentLine
{
    public ICollection<Point> Points { get; protected set; }

    public int DeltaX { get; protected set; }
    public int DeltaY { get; protected set; }

    public bool IsDiagonal => DeltaX != 0 && DeltaY != 0;

    private static Point ParsePoint(string input)
    {
        var coords = input.Split(",");
        return new Point {
            X = int.Parse(coords[0]),
            Y = int.Parse(coords[1])
        };
    }

    public bool Intersects(int x, int y)
    {
        return Points.Any(p => p.X == x && p.Y == y);
    }

    private static int StepSize(int x1, int x2)
    {
        var diff =  x2 - x1;
        if (diff == 0) return 0;
        return diff / Math.Abs(diff); // transforms -4 to -1 but also 4 to 1
    }

    // Force use of Parse method
    private VentLine() { 
        Points = new Point[] { }; 
    }

    public static VentLine Parse(string input)
    {
        var endPoints = input.Split(" -> ")
            .Select(ParsePoint)
            .OrderBy(p => p.X)
            .ThenBy(p => p.Y)
            .ToArray();

        // now calculate all points this line passes.
        var deltaX = StepSize(endPoints[0].X, endPoints[1].X);
        var deltaY = StepSize(endPoints[0].Y, endPoints[1].Y);
        var points = new List<Point>();

        var x = endPoints[0].X;
        var y = endPoints[0].Y;
        do
        {
            points.Add(new Point(x, y));
            x += deltaX;
            y += deltaY;

        } while (x != endPoints[1].X || y != endPoints[1].Y);
        points.Add(endPoints[1]);

        // ignore direction for now.
        return new VentLine { Points = points, DeltaX = deltaX, DeltaY = deltaY };
    }

    public override string ToString()
    {
        return Points.Any() ?
            $"{Points.First().ToString()} -> {Points.Last().ToString()}"
            : "empty";
    }
}