using System.Drawing;
using System.Text;

namespace Puzzles.Tools;
public class Matrix2d<T>
{
    protected T[][] Data { get; set; }

    public int Width { get; }
    public int Height { get; }

    public int OffsetX { get; set; } = 0;
    public int OffsetY { get; set; } = 0;

    public Matrix2d(int width, int height) 
    {
        Width = width;
        Height = height;

        Data = new T[Height][];

        for(var y = 0; y < Height; y++)
        {
            Data[y] = new T[Width];
        }
    }

    public bool Contains(T needle)
    {
        foreach (var line in Data)
        {
            if (line.Any(other => other?.Equals(needle) == true))
            {
                return true;
            }
        }
        return false;
    }
    
    // Data is a vertical array of rows first.
    // Reverse Y and X access order, to make the matrix easier to debug
    public T this[int x, int y]
    {
        get { return Data[y + OffsetY][x + OffsetX]; }
        set { Data[y + OffsetY][x + OffsetX] = value; }
    }

    public T this[Point p]
    {
        get {  return this[p.X, p.Y]; }
        set { this[p.X, p.Y] = value; }
    }

    public int Count(Func<T, bool> p)
    {
        var count = 0;
        foreach (var line in Data)
        {
            count += line.Count(p);
        }

        return count;
    }

    public bool Any(Func<T, bool> p)
    {
        foreach (var line in Data)
        {
            if (line.Any(p)) return true;
        }

        return false;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach(var line in Data)
        {
            foreach(var item in line)
            {
                sb.Append(item?.ToString() ?? "NULL");
                sb.Append(" ");
            }
            sb.Append(Environment.NewLine);
        }
        return sb.ToString();
    }
}
